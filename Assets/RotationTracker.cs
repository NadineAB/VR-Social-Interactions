using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTracker : MonoBehaviour
{
    public Transform headsetCamera;
    //Holds the previous frames rotation
    Quaternion lastRotation;

    //References to the relevent axis angle variables
    float magnitude;
    Vector3 axis;

    public Vector3 angularVelocity
    {

        get
        {
            //DIVDED by Time.deltaTime to give you the degrees of rotation per axis per second
            return (axis * magnitude) / Time.deltaTime;
        }

    }

    void Start()
    {
        lastRotation = transform.rotation;
    }

    void Update()
    {
        //relevent math
        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(lastRotation);
        deltaRotation.ToAngleAxis(out magnitude, out axis);
        lastRotation = transform.rotation;

    }


}
