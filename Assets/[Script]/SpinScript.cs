using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public Vector3 spinAxis;
    public float rotationAngle = 0.4f;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(spinAxis, rotationAngle);
    }
}
