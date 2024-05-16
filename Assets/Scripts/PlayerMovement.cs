using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;
    private KickBall kickBall;

    private InputAction moveAction, jumpAction;

    private Transform cameraTransform;
    private readonly float rotationSpeed = 5f;
    [SerializeField] private float basicMovementSpeed = 3f;

    private Vector2 input;
    private Vector3 move;

    private readonly float smoothingTime = 0.15f;
    private Vector2 blendFactor;

    private Vector2 currentVelocity;

    [SerializeField] private bool isJumping;
    [SerializeField] private float jumpForce = 30f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        kickBall = GetComponent<KickBall>();


        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!kickBall.isKicking) // should not move when kicking
        {
            input = moveAction.ReadValue<Vector2>();
            blendFactor = Vector2.SmoothDamp(blendFactor, input, ref currentVelocity, smoothingTime);
            Vector3 _lastInput = input;

            if (input == Vector2.zero)
            {
                rb.velocity = Vector3.zero;
                move.x = 0;
                move.z = 0;
            }
            else
            {
                move = new(blendFactor.x, 0, blendFactor.y);
                move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
                move *= basicMovementSpeed;
            }
            
            if (jumpAction.triggered && !isJumping)
            {
                isJumping = true;
                move.y = jumpForce;
            }
            else
                move.y = 0f;

            rb.AddForce(move, ForceMode.Impulse);

            isJumping = false;

            RotateToCameraDirectionWhenMoving(_lastInput);
        }
    }

    private void RotateToCameraDirectionWhenMoving( Vector3 _lastInput)
    {
        // Rotate towards camera direction when moving
        if (_lastInput.sqrMagnitude == 0) return;
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public Quaternion PlayerRotation()
    {
        return transform.rotation;
    }
}
