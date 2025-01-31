using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpheight = 3f;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    Vector3 velocity;
    public bool isGrounded;

    public Animator _animator;

    // Dash variables
    public float dashDistance = 8f; // Distance of the dash
    public float dashCooldown = 3f; // Cooldown time in seconds
    private float dashTimer = 0f; // Tracks time until next dash

    // Update is called once per frame
    void Update()
    {
        // Update dash timer
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = 0;
        float z = 0;

        if (Input.GetKey("a"))
        {
            x += -1;
        }

        if (Input.GetKey("d"))
        {
            x += 1;
        }

        if (Input.GetKey("w"))
        {
            _animator.SetFloat("speed", 1);
            z += 1;
        }

        if (Input.GetKey("s"))
        {
            z += -1;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Dash mechanic
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0 && move.magnitude > 0)
        {
            // Normalize move direction to ensure consistent dash distance
            Vector3 dashDirection = move.normalized;
            controller.Move(dashDirection * dashDistance);
            dashTimer = dashCooldown; // Reset cooldown timer
        }

        // Jump when the space bar is held down and the player is grounded
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
