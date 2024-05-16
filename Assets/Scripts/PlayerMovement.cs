using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;
    private KickBall kickBall;

    private InputAction moveAction;

    private Transform cameraTransform;
    private float rotationSpeed = 0.2f;
    private float basicMovementSpeed = 3f;

    private Vector2 input;
    private Vector3 move;

    private float smoothingTime = 0.15f;
    private Vector2 blendFactor;

    private Vector2 currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        kickBall = GetComponent<KickBall>();


        moveAction = playerInput.actions["Move"];
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!kickBall.isKicking)
        {
            input = moveAction.ReadValue<Vector2>();
            blendFactor = Vector2.SmoothDamp(blendFactor, input, ref currentVelocity, smoothingTime);
            Vector3 _lastInput = input;

            move = new(blendFactor.x, 0, blendFactor.y);
            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move *= basicMovementSpeed;
            move.y = 0f;

            rb.AddForce(move, ForceMode.Impulse);

            // Rotate towards camera direction when moving
            if (_lastInput.sqrMagnitude == 0) return;
            float targetAngle = cameraTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
