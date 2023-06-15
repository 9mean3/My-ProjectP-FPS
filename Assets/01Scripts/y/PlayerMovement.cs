using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 8f;
    //public float jumpHeight = 3f;
    public float gravity = -9.8f;

    public bool isMoving;
    public bool isRunning;

    Gun gun;

    private float curSpeed;
    CharacterController controller;

    public Vector3 velocity;

    private void Awake()
    {
        gun = GetComponent<Gun>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Move();
    }

    private void Move()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            curSpeed = runSpeed;
        }
        else
        {
            curSpeed = walkSpeed;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * curSpeed * Time.deltaTime);

        CheckMoving();

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CheckMoving()
    {
        if (Mathf.Abs(controller.velocity.magnitude) > controller.velocity.magnitude / 4)
        {
            if (Mathf.Abs(controller.velocity.magnitude) > runSpeed / 4)
            {
                isRunning = true;
            }
            isMoving = true;
        }
        else
        {
            isRunning = false;
            isMoving = false;
        }
    }
}
