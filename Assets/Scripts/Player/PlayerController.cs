using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraPosition;

    public float mouseSensivity = 5.0f;
    public float playerSpeed = 5f;
    public float jumpHeight = 2f;

    private float verticalAngle = 0f;
    private float horizontalAngle = 0f;

    private float verticalSpeed = 0f;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLock();
    }

    private void HandleMovement()
    {
        float speed = playerSpeed;

        float moveHorizontal = SimpleInput.GetAxis("Horizontal");
        float moveVertical = SimpleInput.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= speed * Time.deltaTime;

        if (characterController.isGrounded)
        {
            if (SimpleInput.GetButtonDown("Jump"))
            {
                verticalSpeed = Mathf.Sqrt(2f + jumpHeight * Mathf.Abs(Physics.gravity.y));
            }
        }

        verticalSpeed += Physics.gravity.y * Time.deltaTime;

        moveDirection.y = verticalSpeed * Time.deltaTime;

        characterController.Move(moveDirection);
    }

    private void HandleMouseLock()
    {
        float mouseX = SimpleInput.GetAxis("Mouse X") * mouseSensivity;
        float mouseY = -SimpleInput.GetAxis("Mouse Y") * mouseSensivity;

        horizontalAngle += mouseX;
        verticalAngle = Mathf.Clamp(verticalAngle + mouseY, 5f, 45f);

        transform.localRotation = Quaternion.Euler(0f, horizontalAngle, 0f);
        cameraPosition.localRotation = Quaternion.Euler(verticalAngle, 0f, 0f);
    }
}
