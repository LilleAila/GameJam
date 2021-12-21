using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject playerCamera;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float groundVelocity = -3.5f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    /*public float fallDmgVelocity = -10f;
    public float fallDmg = 5;
    public float fallDmgMultiplier = 0.3f;*/

    /*public int sprintSpeedIncrease = 10;
    public int sprintFOVIncrease = 15;*/

    void Start()
    {
        // velocity.y = -200f;
        // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Cursor.lockState = CursorLockMode.Locked;
        playerCamera.GetComponent<Camera>().fieldOfView = SettingsMenu.FOV;

        // Debug.Log(SettingsMenu.difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            PlayerHealth.hp -= 0.1f;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = groundVelocity;
        }

        float walkSpeed = speed;

        /*if(Input.GetKeyDown(KeyCode.LeftControl) && GetComponent<CameraBobbing>().isWalking)
        {
            walkSpeed += sprintSpeedIncrease;
            playerCamera.GetComponent<Camera>().fieldOfView = SettingsMenu.FOV + sprintFOVIncrease;
        } else
        {
            playerCamera.GetComponent<Camera>().fieldOfView = SettingsMenu.FOV;
        }*/

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * walkSpeed * Time.deltaTime);

        /*if(isGrounded && velocity.y >= fallDmgVelocity)
        {
            Debug.Log("[Insert Fall Damage]");
        }*/

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(x != 0 || z != 0)
        {
            GetComponent<CameraBobbing>().isWalking = true;
        } else
        {
            GetComponent<CameraBobbing>().isWalking = false;
        }
    }
}
