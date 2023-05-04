using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Speed Values")]
    public float speed;
    public PlayerCam playerCam;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Vector3 orientedMovement = playerCam.orientation.TransformDirection(movement);
        rb.MovePosition(transform.position + orientedMovement * speed * Time.fixedDeltaTime);
    }
}
