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

    private void Update()
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
            
        }
    }

    private void VelocityCalculationUponButtonPressdown()
    {
        switch (holdDownTime)
        {
            case <= 1:
                Debug.Log("bad");
                kickStrength = 0.2f;
                FinalizeVelocity();
                return;
            case > 1 and <= 1.5f:
                Debug.Log("Decent");
                kickStrength = 0.7f;
                FinalizeVelocity();
                return;
            case > 1.5f and <= 2.5f:
                Debug.Log("OK");
                kickStrength = 1.5f;
                FinalizeVelocity();
                return;
            case > 2.5f and <= 3f:
                Debug.Log("Great");
                kickStrength = 2.2f;
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
        isKicking = false;
    }

    private void FinalizeVelocity()
    {
        Vector3 velocity = new();
        velocity.z = kickStrength * holdDownTime * Time.deltaTime;
        velocityToRigidbody.ReceiveRbVelocityFromKick(velocity);
        clicked = false;
        holdDownTime = 0f;
        isKicking = false;
    }
}
