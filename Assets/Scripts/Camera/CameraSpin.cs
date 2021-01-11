using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// The purpose of this script is to rotate the camera so that the bottom is always pointing towards the planet, as long as the shop is within it's graviational field

public class CameraSpin : MonoBehaviour
{
    // Below are public variables
    public GameObject inFieldOf = null;
    public GameObject target;
    // Below are private variables
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowShip();
        if (inFieldOf != null)
        {
            RotateToPlanet();
        } else
        {
            RotateToShip();
        }
    }

    void RotateToPlanet()
    {  // If in a planets field, it should always be below
        // First calculate the vector from camera to planet, the opposite vector of that will rotate the bottom of the camera to face planet
        Vector2 direc = -(inFieldOf.transform.position - target.transform.position);
        transform.rotation = Quaternion.Euler(direc);
    }

    void RotateToShip()
    {  // If not in a planets field, then rotate according to bottom of ship

    }

    void FollowShip()
    {  // This method is responsible for following the ship
        Transform targetTrans = target.transform;
        transform.position = new Vector3(targetTrans.position.x, targetTrans.position.y, transform.position.z);
    }
}
