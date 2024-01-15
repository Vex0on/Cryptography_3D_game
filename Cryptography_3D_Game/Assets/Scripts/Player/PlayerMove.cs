using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float runSpeed = 15f;
    [SerializeField] Animator animator;

    Vector2 moveInput;
    Rigidbody rb;
    bool isModalOpen = false;

    private Coroutine inputChecker;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isModalOpen)
        {
            HandleMovement();
            UpdateAnimator();
        }
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

    private void OnEquipment(InputValue value)
    {
        if (Keyboard.current.iKey.isPressed)
        {
            SetPlayerMovementEnabled(!isModalOpen);

            if (isModalOpen)
            {
                if (inputChecker != null)
                    StopCoroutine(inputChecker);
                inputChecker = StartCoroutine(CustomInputCheck());
            }

            StartCoroutine(ToggleEquipmentModalAfterInputCheck());
        }
    }

    private IEnumerator ToggleEquipmentModalAfterInputCheck()
    {
        yield return new WaitForEndOfFrame();

        EquipmentModalManager.Instance.ToggleEquipmentModal();
    }

    private IEnumerator CustomInputCheck()
    {
        yield return new WaitForSeconds(0.2f);

        while (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            while (Keyboard.current.leftCtrlKey.isPressed && Keyboard.current.backspaceKey.isPressed)
            {
                Debug.Log("Wprowadzono 'i' w inpucie podczas otwartego modala");

                yield return new WaitForSeconds(0.2f);
            }

            yield return null;
        }
    }


    private void SetPlayerMovementEnabled(bool isEnabled)
    {
        if (!isEnabled)
        {
            moveInput = Vector2.zero;
        }

        enabled = isEnabled;
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
