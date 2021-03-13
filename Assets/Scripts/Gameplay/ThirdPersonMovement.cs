using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;

    [SerializeField] float speed = 6.0f;

    [SerializeField] float turnSmoothTime = 0.1f;
    float turnSmoothVelcocity;

    [SerializeField] bool isGrounded;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundmask;
    [SerializeField] float gravity;

    Vector3 velocity;

    Animator anim;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        isGrounded = Physics.CheckSphere(player.position, groundCheckDistance, groundmask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("isWalking", true);
            //get the angle to rotate the character
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            //smooth rotation of the character
            float angle = Mathf.SmoothDampAngle(player.eulerAngles.y, targetAngle, ref turnSmoothVelcocity, turnSmoothTime);

            //rotate target with the direction
            player.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            //switch the direction of the player with coordiates of the camera position
            Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(movDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
