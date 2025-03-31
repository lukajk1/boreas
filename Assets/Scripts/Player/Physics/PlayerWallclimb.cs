using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallclimb : MonoBehaviour
{
    [SerializeField] private PlayerLookAndMove pMovement;
    [SerializeField] private Transform playerLookAndMoveTransform;
    [SerializeField] private Rigidbody rb;

    private GameObject player;
    private HUDManager hudManager;

    private float wallClimbDuration = 0.5f;
    private float wallClimbTimer = 0f;
    private bool canWallClimb = true;

    public bool IsWallClimbing = false;

    private void OnEnable()
    {
        pMovement.OnGroundedChanged += HandleGroundedChanged;
    }

    private void OnDisable()
    {
        pMovement.OnGroundedChanged -= HandleGroundedChanged;
    }

    private void Start()
    {
        hudManager = FindFirstObjectByType<HUDManager>();
        player = transform.root.gameObject;
    }

    private void Update()
    {
        Vector3 rot = playerLookAndMoveTransform.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rot.y, 0f);
    }

    private void FixedUpdate()
    {
        if (!canWallClimb) return;

        if (Keyboard.current.spaceKey.isPressed && !pMovement.IsGrounded)
        {
            Vector3 origin = transform.position;
            Vector3 up = transform.up;
            Vector3 forward = transform.forward;
            float rayLength = 1f;

            Vector3 bottom = origin + up * -0.4f;
            Vector3 middle = origin;
            Vector3 top = origin + up * 0.4f;

            bool bottomHit = Physics.Raycast(bottom, forward, rayLength);
            bool middleHit = Physics.Raycast(middle, forward, rayLength);
            bool topHit = Physics.Raycast(top, forward, rayLength);

            Debug.DrawLine(bottom, bottom + forward * rayLength, Color.red);
            Debug.DrawLine(middle, middle + forward * rayLength, Color.green);
            Debug.DrawLine(top, top + forward * rayLength, Color.blue);

            if (bottomHit || middleHit || topHit)
            {
                hudManager.SetWallClimbMeter(true);
                IsWallClimbing = true;

                wallClimbTimer += Time.fixedDeltaTime;
                if (wallClimbTimer >= wallClimbDuration)
                {
                    canWallClimb = false;
                }
                else
                {
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, 10f, rb.linearVelocity.z);
                }

                hudManager.SetWallClimbStamina((wallClimbDuration - wallClimbTimer) / wallClimbDuration);
            }
            else
            {
                IsWallClimbing = false;
            }
        }
    }

    private void HandleGroundedChanged(bool grounded)
    {
        if (grounded)
        {
            wallClimbTimer = 0f;
            canWallClimb = true;
            hudManager.SetWallClimbMeter(false);
        }
    }
}
