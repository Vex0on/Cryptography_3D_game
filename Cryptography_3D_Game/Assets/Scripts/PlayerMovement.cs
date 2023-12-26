using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;

    PlayerInput input;

    Vector2 currentMovement;
    bool movePressed;
    bool runPressed;

    Camera playerCamera;
    Transform playerTransform;

    float rotationX = 0f;
    float rotationY = 0f;
    public float sensitivity = 2f;

    private void Awake()
    {
        input = new PlayerInput();

        input.CharacterControlls.Move.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movePressed = currentMovement.x != 0 || currentMovement.y != 0;
        };

        input.CharacterControlls.Move.canceled += ctx => movePressed = false;

        input.CharacterControlls.Run.performed += ctx => runPressed = true;
        input.CharacterControlls.Run.canceled += ctx => runPressed = false;

        input.CharacterControlls.Look.performed += ctx => RotateCamera(ctx.ReadValue<Vector2>());
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        playerCamera = Camera.main;
        playerTransform = transform;
    }

    void Update()
    {
        HandleMovement();
        UpdateCameraPosition();
    }

    void HandleMovement()
    {
        // Jeœli animacja jest odpowiedzialna za poruszanie postaci, nie dodawaj dodatkowego przesuniêcia

        // Aktywuj animacjê chodzenia
        if (movePressed)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Aktywuj animacjê biegu
        animator.SetBool("isRunning", runPressed);

        // Obrót postaci wokó³ osi Y
        float targetRotationY = Mathf.Atan2(currentMovement.x, currentMovement.y) * Mathf.Rad2Deg;
        float rotationSpeedY = 5f;
        float newRotationY = Mathf.LerpAngle(playerTransform.eulerAngles.y, targetRotationY, rotationSpeedY * Time.deltaTime);
        playerTransform.rotation = Quaternion.Euler(0f, newRotationY, 0f);
    }

    void UpdateCameraPosition()
    {
        Vector3 cameraOffset = new Vector3(0f, 1.5f, -2f);
        playerCamera.transform.position = playerTransform.position + cameraOffset;
    }

    void RotateCamera(Vector2 input)
    {
        float mouseX = input.x * sensitivity;
        float mouseY = input.y * sensitivity;

        rotationY += mouseX;
        playerTransform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    private void OnEnable()
    {
        input.CharacterControlls.Enable();
    }

    private void OnDisable()
    {
        input.CharacterControlls.Disable();
    }
}
