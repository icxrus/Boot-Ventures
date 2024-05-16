using UnityEngine;

public class ApplyVelocityToRigidbody : MonoBehaviour
{
    Rigidbody rb;
    Vector3 ballVelocity;

    private PlayerMovement playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
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
        transform.rotation = playerMovement.PlayerRotation();
    }

    public void StopBallMovement()
    {
        ballVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }
}
