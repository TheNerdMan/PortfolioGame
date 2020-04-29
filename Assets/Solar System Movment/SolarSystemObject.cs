using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemObject : MonoBehaviour
{
    
    public float mass;
    public float radius;
    public Vector3 initialVelocity;
    Vector3 currentVelocity;

    void Awake () {
        currentVelocity = initialVelocity;
    }

    public void UpdateVelocity (SolarSystemObject[] allObjects, float timeStep) {
        foreach (var solarObject in allObjects)
        {
            if (solarObject != this)
            {
                float sqrDst = (solarObject.GetComponent<Rigidbody>().position - GetComponent<Rigidbody>().position).sqrMagnitude;
                Vector3 forceDir = (solarObject.GetComponent<Rigidbody>().position - GetComponent<Rigidbody>().position).normalized;
                Vector3 force = forceDir * Universe.gravitationalConstant * mass * solarObject.mass / sqrDst;
                Vector3 acceleration = force / mass;
                currentVelocity += acceleration * timeStep;
            }
        }
    }

    public void UpdatePosition (float timeStep) {
        GetComponent<Rigidbody>().position += currentVelocity * timeStep;
    }
}
