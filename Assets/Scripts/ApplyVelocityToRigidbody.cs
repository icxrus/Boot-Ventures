using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyVelocityToRigidbody : MonoBehaviour
{
    Rigidbody rb;
    Vector3 ballVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    void Update()
    {
        if (ballVelocity != Vector3.zero)
        {
            rb.velocity = ballVelocity;
        }
    }

    public void ReceiveRbVelocityFromKick(Vector3 velocity)
    {
        ballVelocity = velocity;
    }
}
