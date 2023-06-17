using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public UnityEvent walking;
    public UnityEvent running;

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
        if (isMoving && !isRunning)
        {
            walking.Invoke();
        }
        else if (isRunning)
        {
            running.Invoke();
        }

        Move();
    }

    private void Move()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if(move.magnitude > 1)
        move.Normalize();

        controller.Move(move * curSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && !gun.isShooting && z > 0)
        {
            curSpeed = runSpeed;
        }
        else
        {
            curSpeed = walkSpeed;
        }

        CheckMoving();

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void CheckMoving()
    {
        //Debug.Log(Mathf.Abs(controller.velocity.magnitude));
        if (Mathf.Abs(controller.velocity.magnitude) > walkSpeed / 4)
        {
            isMoving = true;
        }
        else
            isMoving = false;
        if (Mathf.Abs(controller.velocity.magnitude) > walkSpeed * 1.2f)
        {
            isRunning = true;
        }
        else
            isRunning = false;
    }
}
