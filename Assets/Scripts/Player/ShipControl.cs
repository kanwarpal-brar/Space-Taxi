using UnityEngine;

public class ShipControl : MonoBehaviour
{   // Setup variables availabale to inspector
    public float baseAccel = 150f;
    public float baseRotSpeed = 120f;
    public float dampeningFactor = 100f;
    public float maxSpeed = 10f;
    public float maxJumpDistance = 10f;  // The maximum distance the player can be from the planet to jump to the other side
    public bool gravExemption = false;
    public float jumpFacingSensitivity = 0.5f;
    // Setup private variables
    private Rigidbody2D rb;
    private float[] xy = new float[2];
    private GameObject inFieldOf = null;  // Represents the planet who's gravitational field you are currently in
    private Collider2D bodyCollider;
    private bool bringToStop = false;
    private bool isJumping = false;
    private Vector2 jumpTarget; // Is the target jump location
    private bool isFacingTarget = false;
    private double G = 6.67408 * Mathf.Pow(10, -11);
    private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<Collider2D>();
        particle = GameObject.Find("Thrust").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input and set it to an xy array every frame
        xy[0] = Input.GetAxis("Horizontal");
        xy[1] = Input.GetAxis("Vertical");

        // Below is voodoo which is reversed for some reason
        if (!Input.GetKey(KeyCode.W))
        {
            particle.Play(true);
        }
    }

    private void FixedUpdate()
    {
        GravitationalAttraction();  // Check gravity
        // Very first thing to check is if a jump is being done, input is registered differently
        if (isJumping)
        {
            if (isFacingTarget)
            {
                MoveToJump();
            } else
            {
                FaceToTarget();
            }
            if (jumpTarget == (Vector2)transform.position)  // Backuo in case the ship gets stuck in jump mode
            {
                isJumping = false;
            }
        } else
        {
            // Below checks inputs for planet jump
            if (!isJumping && Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("S");
                crossPlanet();  // If s is pressed, attempt to cross the planet
            }

            // Below checks inputs for speed dampener
            if (Input.GetButton("Jump"))  // The jump button is currently assigned to space and used as a break
            {
                bringToStop = true;
            }
            else
            {
                bringToStop = false;
            }
            // Apply rotations and position changes on the ship
            rotateShip();
            movement();
            if (bringToStop)
            {
                applyStoppingForce();
            }
        }
    }

    private void movement()  // Movement is based on forces Do not use time.deltaTime with forces as they are not based on time (they are automatically interpolated)
    {
        if (xy[1] > 0)
        {
            rb.AddForce(transform.up * baseAccel);
        }
        // Clamp the maximum possible speed in each vector direction
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));

    }

    private void MoveToJump()  // This functions adds velocity to the ship in order to move to a specific location
    {
        // First thing to do is check if the jump target exists, then check if the ship is facing the target or not
        if (jumpTarget != null && Vector2.Distance((Vector2)transform.up.normalized, (Vector2)(jumpTarget-(Vector2)transform.position).normalized) < jumpFacingSensitivity)
        {
            // Now velocity can be added. It is assumed that the ship is already directed in the correct direction
            transform.position = jumpTarget;
            isFacingTarget = false;
            isJumping = false;
            Debug.Log("Done");
        }
        /*
        if (jumpTarget != null && (Vector2)transform.up.normalized == (Vector2)(jumpTarget-(Vector2)transform.position).normalized)
        {
            Debug.Log("Facing");
            // Now velocity can be added. It is assumed that the ship is already directed in the correct direction
            rb.MovePosition(transform.position * jumpTarget);
        }*/

    }

    private void FaceToTarget()  // This method will make the ship slow to face the target
    {
        // Kanwarpal from 9:30 pm here, a turkish person is bothering me
        if (jumpTarget != null && Vector2.Distance((Vector2)transform.up.normalized, (Vector2)(jumpTarget - (Vector2)transform.position).normalized) > jumpFacingSensitivity)
        {
            if ((Vector2)transform.up.normalized == (Vector2)(jumpTarget - (Vector2)transform.position).normalized)
            {
                Debug.Log("Jaydon");
            }
            Quaternion direc = Quaternion.Euler(((Vector2)inFieldOf.transform.position - (Vector2)transform.position) * baseRotSpeed * 0.1f);
            rb.MoveRotation(direc);  // Apply rotation from starting rotation
        } else
        {
            Debug.Log("Facing");
            isFacingTarget = true;
        }
    }

    public void applyStoppingForce()
    {
        if (rb.velocity.magnitude!=0)
        {
            rb.AddForce(-rb.velocity.normalized * dampeningFactor);  // Applies a dampening force to oppose
        }
    }

    public void fullStop()
    {
        if (rb.velocity.magnitude != 0)
        {
            rb.AddForce(-rb.velocity.normalized * dampeningFactor*4);  // Applies a dampening force to oppose, this is 4x stronger than usual for an instant stop
        }
    }
    private void rotateShip()
    {
        Quaternion turn = transform.rotation * Quaternion.Euler(new Vector3(0,0,-xy[0]).normalized * baseRotSpeed* Time.fixedDeltaTime);
        rb.MoveRotation(turn);
    }

    private void crossPlanet()
    {
        if (inFieldOf != null)  // Check if actually in field of something
        {
            // Given that sufficient velocity is present, and the ship is in it's max jump range
            if (rb.velocity.magnitude <= 0.5 && Vector2.Distance(transform.position, inFieldOf.transform.position) < maxJumpDistance)
            {
                Debug.Log("Starting");
                isJumping = true;  // Enable jumping
                // I also need to set the jump target on the opposite side of the planet from here (How do I calculate that?)
                Vector2 direction = inFieldOf.transform.position - transform.position;  // represents vector between self and planet
                // In the directon of the planet target, move a distance equal to the distance between planet and self
                jumpTarget = direction.normalized * Vector2.Distance((Vector2)inFieldOf.transform.position, (Vector2)transform.position);  // This will jump twice the distance between the ship and planet in the direction of the opposite side of the planet
            }
            else
            {
                Debug.Log("Too Fast to jump, or too far from planet");
          }
        }
    }

    private void GravitationalAttraction()
    {
        if (inFieldOf != null && !gravExemption)
        {
            Vector2 gravDirection = (Vector2)inFieldOf.transform.position - (Vector2)transform.position;
            float gravMag = (float)((G * inFieldOf.GetComponent<PlanetController>().mass * rb.mass) / Mathf.Pow(Vector2.Distance((Vector2)transform.position, inFieldOf.transform.position), 2));
            // The force is multiplied by 2 because the planet exerts force on ship, as does the ship on planet
            rb.AddForce(2*gravDirection.normalized * gravMag);
        }
    }

    // Trigger-related functions
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Planet")  // check if colliding with a planet
        {
            inFieldOf = other.gameObject;
            AlertCameraSpinField(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Planet")  // check if colliding with a planet
        {
            inFieldOf = null;  // No longer in any field
            AlertCameraSpinNull(other.gameObject);
        }
    }

    void AlertCameraSpinField(GameObject obj)  // This method passes data to the camera spin script
    {
        CameraSpin script = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSpin>();
        script.inFieldOf = obj;
    }

    void AlertCameraSpinNull(GameObject obj)  // This method passes data to the camera spin script
    {
        CameraSpin script = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSpin>();
        script.inFieldOf = null;
    }
}
