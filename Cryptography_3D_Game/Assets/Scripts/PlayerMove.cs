using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float runSpeed = 15f;
    [SerializeField] Animator animator;

    Vector2 moveInput;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        UpdateAnimator();
    }

    private void HandleMovement()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x, 0, moveInput.y);
        playerVelocity.Normalize();
        Vector3 moveDirection = transform.TransformDirection(playerVelocity);

        float speed = walkSpeed;

        if (Keyboard.current.leftShiftKey.isPressed)
        {
            speed = runSpeed;
        }

        rb.velocity = moveDirection * speed;
    }

    private void UpdateAnimator()
    {
        float moveMagnitude = new Vector2(moveInput.x, moveInput.y).magnitude;
        animator.SetBool("isWalking", moveMagnitude > 0); 
        animator.SetBool("isRunning", moveMagnitude > 0 && Keyboard.current.leftShiftKey.isPressed);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
