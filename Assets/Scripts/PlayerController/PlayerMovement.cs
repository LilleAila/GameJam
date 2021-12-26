using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject playerCamera;

    public float speed = 12f;
    public float sprintSpeed = 15f;
    [Range(0, 1)]public float sprintHp = 0.01f;
    public float gravity = -9.81f;
    public float groundVelocity = -3.5f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public static bool sprinting = false;
    float sprintTime = 0;

    public GameObject model;
    public float walkAnimSpeed = 1.3f;
    public float sprintAnimSpeed = 2.3f;

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
        if(InputManager.GetKeyDown("Submit"))
        {
            PlayerHealth.hp -= 0.1f;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = groundVelocity;
        }

        float walkSpeed = speed;
        if (InputManager.GetKey("Sprint"))
        {
            walkSpeed = sprintSpeed;
            sprinting = true;
            sprintTime += Time.deltaTime;
            if(sprintTime >= 5)
            {
                PlayerHealth.hp -= sprintHp * Time.deltaTime;
            }
        } else
        {
            sprinting = false;
            sprintTime = 0;
        }

        /* float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical"); */

        float x = InputManager.GetAxis("Side");
        float z = InputManager.GetAxis("Forward");

        if(x != 0 || z != 0)
        {
            if (sprinting) model.GetComponent<Animator>().speed = sprintAnimSpeed;
            else model.GetComponent<Animator>().speed = walkAnimSpeed;
        } else
        {
            model.GetComponent<Animator>().speed = 0;
        }


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * walkSpeed * Time.deltaTime);

        /*if(isGrounded && velocity.y >= fallDmgVelocity)
        {
            Debug.Log("[Insert Fall Damage]");
        }*/

        if(InputManager.GetKeyDown("Jump") && isGrounded)
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
