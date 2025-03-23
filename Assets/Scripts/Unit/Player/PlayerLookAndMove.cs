using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerLookAndMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera mainCamera; 

    [SerializeField] private InputSystem_Actions actions;

    [SerializeField] private Rigidbody rb; 
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform lastJumpedFrom;

    private float moveSpeed = 6f;
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            if (value > 0) 
            {
                moveSpeed = value;
            }
        }
    }

    private float jumpForce = 480f;
    public float JumpForce
    {
        get
        {
            return jumpForce;
        }
        set
        {
            if (value > 0)
            {
                jumpForce = value;
            }
        }
    }
    private float initJumpForce;
    private float initMoveSpeed;

    private float sensitivity = 190f;
    private float xRotation;
    private float yRotation;

    private bool isGrounded;

    private InputAction move;
    private InputAction jump;

    private Game game;

    private float positionSaveInterval = 0.5f;
    private float timeSinceLastSave = 0f;
    private void Awake()
    {
        initJumpForce = JumpForce;
        initMoveSpeed = MoveSpeed;

        actions = new InputSystem_Actions(); 

        rb = player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnEnable()
    {
        move = actions.Player.Move;
        move.Enable();

        jump = actions.Player.Jump;
        jump.Enable();

        jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        move.Disable();

        jump.performed -= OnJumpPerformed;
        jump.Disable();
    }
    private void Start()
    {
        game = Game.Instance;
    }
    private void FixedUpdate()
    {
        timeSinceLastSave += Time.fixedDeltaTime;

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, .35f);
        //Debug.Log(isGrounded);

        if (isGrounded)
        {
            if (hit.collider.gameObject.layer == 4)
            {
                player.transform.position = lastJumpedFrom.position;
            }
            else
            {
                if (timeSinceLastSave > positionSaveInterval)
                {
                    lastJumpedFrom.position = rb.position;
                    timeSinceLastSave = 0f;
                }
            }
        }
    }

    private void Update()
    {
        if (!Game.IsPaused && !Game.IsInDialogue)
        {
            Vector3 movement = DetermineMovementVector();
            rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

            DetermineCamMovement();
        }

    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isGrounded && !Game.IsPaused) // possibly do a check to Mathf.Abs(rb.linearVelocity.y) < 0.01f but this could return true in cases where it shouldn't
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset vertical velocity
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void DetermineCamMovement()
    {
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // prevent looking above/below 90

        mainCamera.transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
    }
    private Vector3 DetermineMovementVector()
    {
        Vector2 moveDir = move.ReadValue<Vector2>().normalized * MoveSpeed;

        // Get only the horizontal (yaw) rotation of the player, ignoring the pitch (up/down)
        float yRotation = transform.eulerAngles.y;

        // Calculate movement relative to the player's current horizontal rotation
        Vector3 forward = new Vector3(Mathf.Sin(yRotation * Mathf.Deg2Rad), 0, Mathf.Cos(yRotation * Mathf.Deg2Rad)) * moveDir.y;
        Vector3 right = transform.right * moveDir.x;

        Vector3 combined = forward + right;
        return new Vector3(combined.x, 0, combined.z);
    }


    public void ResetValues()
    {
        JumpForce = initJumpForce;
        MoveSpeed = initMoveSpeed;
    }

}
