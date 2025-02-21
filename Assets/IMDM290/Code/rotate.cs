using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationDuration = 9f; //duration of rotation
    public Vector3 rotationAxis = Vector3.up; //Makes it rotate up/y-axis

    // Update is called once per frame
    void Update()
    {
        float rotationAnglePerSec = 360f / rotationDuration; // calculates rotation

        float rotationAngleThisFrame = rotationAnglePerSec * Time.deltaTime; 
        
        transform.Rotate(rotationAxis, rotationAngleThisFrame, Space.World);
    }
}
