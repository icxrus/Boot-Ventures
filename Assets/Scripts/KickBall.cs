using UnityEngine.InputSystem;
using UnityEngine;

public class KickBall : MonoBehaviour
{
    private ApplyVelocityToRigidbody velocityToRigidbody;
    private PlayerInput controls;

    private InputAction leftClick;
    private bool leftClickDown;
    private bool clicked;
    public bool isKicking;

    [SerializeField]
    private float holdDownTime;
    private float kickStrength;

    void Start()
    {
        velocityToRigidbody = GameObject.FindGameObjectWithTag("Ball").GetComponent<ApplyVelocityToRigidbody>();
        controls = GetComponent<PlayerInput>();
        leftClick = controls.actions["Click"];
    }
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (!isKicking)
            {
                isKicking = true;
            }
        }
    }

    private void FixedUpdate()
    {
        //Check for holding down of Left Click
        leftClick.performed += _ => leftClickDown = true;
        leftClick.canceled += _ => leftClickDown = false;

        if (isKicking)
        {
            TimeClickHoldDown();

            if (!leftClickDown && clicked)
            {
                VelocityCalculationUponButtonPressdown();
            }
            isKicking = false;
        }
    }

    private void VelocityCalculationUponButtonPressdown()
    {
        switch (holdDownTime)
        {
            case <= 1:
                Debug.Log("bad");
                kickStrength = 1.2f;
                FinalizeVelocity();
                return;
            case > 1 and <= 1.5f:
                Debug.Log("Decent");
                kickStrength = 1.7f;
                FinalizeVelocity();
                return;
            case > 1.5f and <= 2.5f:
                Debug.Log("OK");
                kickStrength = 2.5f;
                FinalizeVelocity();
                return;
            case > 2.5f and <= 3f:
                Debug.Log("Great");
                kickStrength = 3.2f;
                FinalizeVelocity();
                return;
            default:
                Debug.Log("Missed!");
                velocityToRigidbody.ReceiveRbVelocityFromKick(Vector3.zero);
                clicked = false;
                holdDownTime = 0f;
                return;
        }

    }

    private void TimeClickHoldDown()
    {
        if (leftClickDown)
        {
            holdDownTime += Time.deltaTime;
            clicked = true;
        }
    }

    private void FinalizeVelocity()
    {
        Vector3 velocity = new Vector3(transform.position.x - velocityToRigidbody.transform.position.x,0, transform.position.z - velocityToRigidbody.transform.position.z);
        velocity *= -kickStrength; // * holdDownTime;
        velocityToRigidbody.ReceiveRbVelocityFromKick(velocity);
        clicked = false;
        holdDownTime = 0f;
    }
}
