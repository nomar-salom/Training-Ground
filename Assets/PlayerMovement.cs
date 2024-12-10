using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;       
    private CharacterController controller;
    public float gravity = -9.8f; 
    private Vector3 velocity;
    public float jumpHeight = 5f;
    private bool groundBelowPlayer;
    public Camera playerCamera;

    public float horizontalLimit = 60f; 
    //camera sensitivity
    public float sensitivity = 5f;
    //Vertical change in camera
    private float rotationX = 0f; 
    public bool shoot;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {   
        groundBelowPlayer = controller.isGrounded;
        //Adjusts vertical velocity  when there is ground beneath player (stops bouncing)
        if(groundBelowPlayer && velocity.y < 0) {
            velocity.y = -2f;
        }

        //Gets movement if moving left or right (A or D keys)
        float moveLeftRight = Input.GetAxis("Horizontal"); 

        //Gets movement if moving forward or backward (W or S keys)
        float moveForwardBackward = Input.GetAxis("Vertical");

        //calculates direction of player based on input
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 move = right * moveLeftRight + forward * moveForwardBackward;

        //calculates how player moves based on input
        Vector3 amtOfMovement = move * speed * Time.deltaTime;
        controller.Move(amtOfMovement);   

        if(groundBelowPlayer && Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("We're jumping and popping");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if(!groundBelowPlayer) {
            velocity.y = velocity.y + (gravity * Time.deltaTime);
        }
        controller.Move(velocity * Time.deltaTime);

        //handles camera movements based on inputs and sensitivity
        if(playerCamera != null) {
            float mouseVerticalInput = Input.GetAxis("Mouse Y") * sensitivity;
            rotationX = rotationX - mouseVerticalInput;
            //clamping helps in avoiding the camera flipping like crazy
            rotationX = Mathf.Clamp(rotationX, -horizontalLimit, horizontalLimit); 
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

            //horizontal rotation taking into account input/sensitivity
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity);
        }

        if(!PauseMenu.isPaused) {
            shoot = Input.GetMouseButtonDown(0);
        }
            
    }
}
