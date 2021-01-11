using UnityEngine;

public class PlanetController : MonoBehaviour
{
    // Public variables for inspector
    public float mass = 10000f;
    // Private variables for use
    private Rigidbody2D rb;
    private Rigidbody2D inField;  // Represents a player object in graviational field
    private ShipControl playerScript;
    private double G = 6.67408 * Mathf.Pow(10, -11);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the attached rigidbody (in the case of planets, only used for mass
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()  // Physics tick
    {
        exertForce();
    }

    private bool playerInField(GameObject obj)
    {
        if (obj.tag.Equals("Player"))
        {
            return true;
        } else
        {
            return false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerInField(other.gameObject)) {
            inField = other.gameObject.GetComponent<Rigidbody2D>();
            playerScript = inField.GetComponent<ShipControl>();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (playerInField(other.gameObject))
        {
            inField = null;
        }
    }

    private void exertForce()  // Will exert a force on the object in field
    {

    }
}
