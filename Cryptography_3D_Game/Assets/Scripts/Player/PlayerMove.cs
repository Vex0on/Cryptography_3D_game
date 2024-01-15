using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float runSpeed = 15f;
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_InputField messageInputField;

    private Vector2 moveInput;
    private Rigidbody rb;
    private bool isModalOpen = false;
    private bool isMessageInputFocused = false;

    private Coroutine inputChecker;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (messageInputField != null)
        {
            messageInputField.onSelect.AddListener(OnMessageInputSelect);
            messageInputField.onDeselect.AddListener(OnMessageInputDeselect);
        }
    }

    void Update()
    {
        if (!isModalOpen || isMessageInputFocused)
        {
            HandleMovement();
            UpdateAnimator();
        }
    }

    private void OnMessageInputSelect(string value)
    {
        isMessageInputFocused = true;
    }

    private void OnMessageInputDeselect(string value)
    {
        isMessageInputFocused = false;
    }

    private void HandleMovement()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Vector3 moveDirection = transform.TransformDirection(playerVelocity);

        float speed = Keyboard.current.leftShiftKey.isPressed ? runSpeed : walkSpeed;
        rb.velocity = moveDirection * speed;
    }

    private void UpdateAnimator()
    {
        float moveMagnitude = moveInput.magnitude;
        animator.SetBool("isWalking", moveMagnitude > 0);
        animator.SetBool("isRunning", moveMagnitude > 0 && Keyboard.current.leftShiftKey.isPressed);
    }

    private void OnEquipment(InputValue value)
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            if (!isMessageInputFocused)
            {
                SetPlayerMovementEnabled(!isModalOpen);

                if (isModalOpen && inputChecker != null)
                {
                    StopCoroutine(inputChecker);
                    inputChecker = StartCoroutine(CustomInputCheck());
                }

                StartCoroutine(ToggleEquipmentModalAfterInputCheck());
            }
        }
    }

    private IEnumerator ToggleEquipmentModalAfterInputCheck()
    {
        yield return new WaitForEndOfFrame();
        EquipmentModalManager.Instance.ToggleEquipmentModal();
    }

    private IEnumerator CustomInputCheck()
    {
        while (isModalOpen)
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
