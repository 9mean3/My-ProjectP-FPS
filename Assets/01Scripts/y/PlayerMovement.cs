using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 8f;
    //public float jumpHeight = 3f;
    public float gravity = -9.8f;

    [SerializeField] private Animator animator;

    private float curSpeed;
    CharacterController controller;

    public Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded && velocity.y < 0)
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

/*        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        Debug.Log(controller.isGrounded); */  

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

/*        if(velocity.magnitude > 0.1f)
        {
            animator.SetBool("isMoving", true);
        }
        else
            animator.SetBool("isMoving", false);*/
    }
}
