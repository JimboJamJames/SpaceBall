using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    // Use this for initialization
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up, Time.deltaTime);     // mul*deltaTime by some speed
        rb.AddTorque(Vector3.up, ForceMode.Acceleration); // control speed with angular drag and mass
    }
}