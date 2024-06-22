using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 7f;
    public float currentVelocity;
    public float gravity = -9.81f;
    public float gravityMutiplier = 3.0f;
    public float jumpForce;
    public float velocity;
    public Vector2 input;
    public Vector3 movement;
    public Transform mCamera;
    PlayerInput playerInput;
    CharacterController characterController;
    private Cinemachine.CinemachineVirtualCamera virtualCamera;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Mov.performed += Move;
        playerInput.PlayerControls.Mov.canceled += Move;
        playerInput.PlayerControls.Jump.started += Jump;
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        mCamera = Camera.main.transform;
        virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }
    }

    void Update()
    {
        Gravity();
        Movement();
        RotateToCamera();
    }

    private void Movement()
    {
        Vector3 forward = mCamera.forward;
        Vector3 right = mCamera.right;
        
        forward.y = 0f;
        right.y = 0f;
        
        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * input.y + right * input.x).normalized;
        characterController.Move(desiredMoveDirection * speed * Time.deltaTime + new Vector3(0, velocity, 0) * Time.deltaTime);
    }

    private void Gravity()
    {
        if (characterController.isGrounded && velocity < 0.0f)
        {
            velocity = -1.0f;
        }
        else
        {
            velocity += gravity * gravityMutiplier * Time.deltaTime;
        }
    }

    private void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (characterController.isGrounded)
        {
            velocity = jumpForce;
        }
    }

    private void RotateToCamera()
    {
        Vector3 forward = mCamera.forward;
        forward.y = 0f;
        forward.Normalize();
        transform.forward = forward;
    }

    void OnEnable()
    {
        playerInput.PlayerControls.Enable();
    }

    void OnDisable()
    {
        playerInput.PlayerControls.Disable();
    }
}