using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 7f;
    public float currentVelocity;
    //public float smoothTime = 0.05f;
    public float gravity = -9.81f;
    public float gravityMutiplier = 3.0f;
    public float velocity;
    public Vector2 input;
    public Vector3 movement;
    public Transform mCamera;
    PlayerInput playerInput;
    CharacterController characterController;
    
    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Mov.performed += Move;
        playerInput.PlayerControls.Mov.canceled += Move;
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        mCamera = Camera.main.transform;
    }

    void Update()
    {
        Gravity();
        Movement();
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, mCamera.eulerAngles.y, transform.eulerAngles.z);
    }

    private void Movement()
    {
        characterController.Move(movement * speed * Time.deltaTime);
    }

    private void Gravity()
    {
        if(characterController.isGrounded && velocity < 0.0f)
        {
            velocity = -1.0f;
        }
        else
        {
            velocity += gravity * gravityMutiplier * Time.deltaTime;
        }
            movement.y = velocity;
    } 

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        movement = new Vector3(input.x, 0f, input.y);
        movement = transform.TransformDirection(movement);
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
