using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBall : MonoBehaviour
{
    ApplyVelocityToRigidbody velocityToRigidbody;

    void Start()
    {
        velocityToRigidbody = GameObject.FindGameObjectWithTag("Ball").GetComponent<ApplyVelocityToRigidbody>();
    }

    //Create function to create velocity based on time pressing down a button to kick the ball and pass it to ApplyVelocityToRigidbody

}
