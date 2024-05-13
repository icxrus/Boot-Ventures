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
            rb.AddForce(ballVelocity, ForceMode.Impulse);
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            ballVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            transform.position = Vector3.up;
        }
#endif
    }

    public void ReceiveRbVelocityFromKick(Vector3 velocity)
    {
        ballVelocity = velocity;
    }
}
