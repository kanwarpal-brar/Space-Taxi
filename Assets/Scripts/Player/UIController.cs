using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// Any given target LZ must have an arrow pointing to it. Alongside this, the planet must always have an arrow pointing to it
public class UIController : MonoBehaviour
{
    // Public Variables
        // An arrayList is used for targets to allow for easier manipulation
    ArrayList targets = new ArrayList(); // Represents all of the Landing Zones that are currently targetted (Will provide a bonus for landing there) [Anything in this should have an arrow towards it]
    // Private Variables
    void Start()
    {
        AddTarget(GameObject.FindGameObjectWithTag("Planet"));  // Add the planet to the arraylist
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AdjustTargetArrows()
    {  // This method adjusts the position of all the target arrows to ensure they keep pointing towards the target

    }

    void AddTarget(GameObject obj)
    {  // This method adds an arrow for a specified gameobject to the targets list, it also adds the object to the arraylist
        targets.Add(obj);

    }
}
