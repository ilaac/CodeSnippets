using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Stats")]
    public float moveSmoothTime;
    public float gravityStrength;
    public float walkSpeed;
    public float runSpeed;

    private CharacterController controller;
    private Vector3 currentMoveVelocity;
    private Vector3 moveDampVelocity;

    private Vector3 currentForceVelocity;

    private Vector3 moveVector;

    private bool isRunningAllowed = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 playerInput = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        };

        if (playerInput.magnitude > 1f)
        {
            playerInput.Normalize();
        }

        moveVector = transform.TransformDirection(playerInput);
        float currentSpeed = isRunningAllowed ? (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) : walkSpeed;

        currentForceVelocity = Vector3.SmoothDamp(
            currentForceVelocity,
            moveVector * currentSpeed,
            ref moveDampVelocity,
            moveSmoothTime
        );

        // Apply gravity separately
        ApplyGravity();

        // Move the character controller using the calculated forces
        controller.Move(currentForceVelocity * Time.deltaTime);
    }

    public float GetCurrentSpeed()
    {
        return currentForceVelocity.magnitude;
    }

    public Vector3 GetMoveVector()
    {
        return moveVector;
    }

    public void UpdateSpeed(bool isADS, Vector3 moveVector)
    {
        float targetSpeed = isADS ? walkSpeed * 0.2f : (isRunningAllowed ? runSpeed : walkSpeed);

        currentForceVelocity = Vector3.SmoothDamp(
            currentForceVelocity,
            moveVector * targetSpeed,
            ref moveDampVelocity,
            moveSmoothTime
        );
    }

    public void SetRunningAllowed(bool allowed)
    {
        isRunningAllowed = allowed;
    }

    void ApplyGravity()
    {
        Ray groundCheckRay = new Ray(transform.position, Vector3.down);

        // Check if the player is grounded
        if (Physics.Raycast(groundCheckRay, 1.1f))
        {
            currentForceVelocity.y = 0f; // If grounded, reset the vertical velocity
        }
        else
        {
            // Apply gravity if not grounded
            currentForceVelocity.y -= gravityStrength * Time.deltaTime;
        }
    }
}
