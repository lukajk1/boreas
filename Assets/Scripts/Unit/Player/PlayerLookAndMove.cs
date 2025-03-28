using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem.Utilities;

public class PlayerLookAndMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera mainCamera; 

    [SerializeField] private InputSystem_Actions actions;

    [SerializeField] private Rigidbody rb; 
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform lastJumpedFrom;

    private float _moveSpeed;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set
        {
            if (value > 0) 
            {
                _moveSpeed = value;
            }
        }
    }

    private float _jumpForce = 13f;
    public float JumpForce
    {
        get => _jumpForce;
        set
        {
            if (value > 0)
            {
                _jumpForce = value;
            }
        }
    }

    private float extraGravityForce = 15f;
    private float airStrafeInfluence = 1.0f; // modify currentSpeedMultiplier in the air instead?
    private float initJumpForce;
    private float initMoveSpeed;
    private float currentSpeedMultiplier = 1f;

    private float sensitivity = 150f;
    private float xRotation;
    private float yRotation;

    private bool isGrounded;
    private bool isCrouching;

    private Game game;
    private PlayerUnit playerUnit;

    private float positionSaveInterval = 0.5f;
    private float timeSinceLastSave = 0f;

    private Vector3 crouchScale = new Vector3(1, 0.4f, 1);
    private Vector3 playerScale;

    private InputAction move;
    private InputAction jumpAction;
    private InputAction crouchAction;

    private Vector3 movementVector;

    private void Awake()
    {
        initJumpForce = JumpForce; // these are here for resetting values after changing them with console commands
        initMoveSpeed = MoveSpeed;

        actions = new InputSystem_Actions(); 

        rb = player.GetComponent<Rigidbody>();
        playerUnit = GetComponent<PlayerUnit>();
        _moveSpeed = playerUnit.BaseMoveSpeed;


        playerScale = player.transform.localScale;
        move = actions.Player.Move;
    }

    private void OnEnable()
    {
        actions.Player.Enable();

        actions.Player.Jump.performed += OnJumpPerformed;
        actions.Player.Crouch.performed += OnCrouchPerformed;
        actions.Player.Crouch.canceled += OnCrouchReleased;
    }

    private void OnDisable()
    {
        actions.Player.Jump.performed -= OnJumpPerformed;
        actions.Player.Crouch.performed -= OnCrouchPerformed;
        actions.Player.Crouch.canceled -= OnCrouchReleased;

        actions.Player.Disable();
    }
    private void Start()
    {
        game = Game.I;

    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(movementVector.x, rb.linearVelocity.y, movementVector.z);

        timeSinceLastSave += Time.fixedDeltaTime;

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, .35f);

        //Debug.Log(isGrounded);

        rb.AddForce(Vector3.down * extraGravityForce, ForceMode.Force);

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

            if (!isCrouching)
            {
                currentSpeedMultiplier = 1f;
            }
        }
        else
        {
            currentSpeedMultiplier = 1.4f;
        }
    }

    private void Update()
    {
        if (!Game.IsPaused && !Game.IsInDialogue)
        {
            movementVector = DetermineMovementVector();

            DetermineCamMovement();
        }

    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isGrounded && !Game.IsPaused) // possibly do a check to Mathf.Abs(rb.linearVelocity.y) < 0.01f but this could return true in cases where it shouldn't
        {

            ForceMode forceMode = ForceMode.Impulse;

            player.transform.localScale = playerScale; // removes crouch mode
            isCrouching = false;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset vertical velocity
            rb.AddForce(Vector3.up * _jumpForce, forceMode);

            //// add force based on vector from movement keys as well
            //Vector2 moveDir = move.ReadValue<Vector2>().normalized * MoveSpeed * currentSpeedMultiplier * (!isGrounded ? airStrafeInfluence : 1f);

            //float yRotation = transform.eulerAngles.y;
            //Vector3 forward = new Vector3(Mathf.Sin(yRotation * Mathf.Deg2Rad), 0, Mathf.Cos(yRotation * Mathf.Deg2Rad)) * moveDir.y;
            //Vector3 right = transform.right * moveDir.x;
            //Vector3 movementVector = forward + right;

            //rb.AddForce(movementVector * _jumpForce * 0.3f, forceMode);


        }
    }
    private void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        if (!Game.IsPaused) 
        {
            player.transform.localScale = crouchScale;
            isCrouching = true;
        }
    }
    private void OnCrouchReleased(InputAction.CallbackContext context)
    {
        if (!Game.IsPaused) 
        {
            player.transform.localScale = playerScale;
            isCrouching = false;
        }
    }

    private void DetermineCamMovement() // I guess this doesn't need to be broken out because where you're looking also determines how movement works?
    {
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // prevent looking above/below 90

        mainCamera.transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
    }
    private Vector3 DetermineMovementVector()
    {

        Vector2 moveDir = move.ReadValue<Vector2>().normalized * MoveSpeed * currentSpeedMultiplier * (!isGrounded ? airStrafeInfluence : 1);

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
