using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles the spawning of passengers to LZ's on the planet
// For balance reasons, passengers can only be spawned on a single LZ at a time
// Each pickup LZ has a target dropoff LZ


public class LZHandler : MonoBehaviour
{
    // Public Variables
    public Transform target;  // Represents the player ship transform (allows you to get position, rotation, etc)
    public GameObject[] LZs;  // Represents all the LZ's on the map
    // Private Variables
    private GameObject[] nearbyLZ;  // This array holds all LZ's close to the player
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;  // Get the transformation of the player ship
        LZs = GameObject.FindGameObjectsWithTag("LZ");  // Get all the tagged Landing Zones
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnPassenger()  // Spawn a random number of passengers on nearby platforms
    {

    }

    private float DistFromPlayer(GameObject obj)
    {  // This method will return the distance from obj to player
        return Vector2.Distance(target.position, obj.transform.position);
    }

    private Vector2 VecFromPlayer(GameObject obj)
    {  // This method returns the vector from the player obj
        return (Vector2)(obj.transform.position - target.position);
    }
}
