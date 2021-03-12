using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;

    [SerializeField] float speed = 6.0f;
    [SerializeField] float jumpHeight = 6f;

    [SerializeField] float turnSmoothTime = 0.1f;
    float turnSmoothVelcocity;

    [SerializeField] LayerMask _groundLayer;
    [SerializeField] bool _isGrounded;
    [SerializeField] Vector3 _checkBoxSize;
    Vector3 _velocity;

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

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("isWalking", true);
            //get the angle to rotate the character
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            //smooth rotation of the character
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelcocity, turnSmoothTime);

            float angle = Mathf.SmoothDampAngle(player.eulerAngles.y, targetAngle, ref turnSmoothVelcocity, turnSmoothTime);

            //rotate target with the direction
            //transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            player.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            //switch the direction of the player with coordiates of the camera position
            Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(movDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
