using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    // Universal Constants
    const double gravitationalConstant = 6.674f * (10 ^ 11);
    const Time physicsTimeStep; // WTF should this be???

    // Universe also moves the bodys
    SolarSystemObject[] solarObjects;

    void Awake () {
        solarObjects = FindObjectsOfType<SolarSystemObject> ();
        Time.fixedDeltaTime = this.physicsTimeStep;
    }

    // Move our solar objects
    void FixedUpdate () {
        for (int i = 0; i < solarObjects.length; i++)
        {
            solarObjects[i].UpdateVelocity(this.physicsTimeStep);
        }

        for (int i = 0; i < solarObjects.length; i++)
        {
            solarObjects[i].UpdatePosition(this.physicsTimeStep);
        }
    }
}
