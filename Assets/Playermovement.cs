using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpheight = 3f;
    public float superJumpHeight = 10f; // Hoogte van de super jump

    public float superJumpCooldown = 5f; // Cooldown van 5 seconden
    private float superJumpTimer = 0f; // Timer voor de super jump

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    Vector3 velocity;
    public bool isGrounded;

    public Animator _animator;

    // Dash variables
    public float dashDistance = 8f;
    public float dashCooldown = 3f;
    private float dashTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Update timers
        if (dashTimer > 0) dashTimer -= Time.deltaTime;
        if (superJumpTimer > 0) superJumpTimer -= Time.deltaTime;

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
            Vector3 dashDirection = move.normalized;
            controller.Move(dashDirection * dashDistance);
            dashTimer = dashCooldown;
        }

        // Normale jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        // Super Jump (Q Key) met cooldown
        if (Input.GetKeyDown(KeyCode.Q) && isGrounded && superJumpTimer <= 0)
        {
            velocity.y = Mathf.Sqrt(superJumpHeight * -2f * gravity);
            superJumpTimer = superJumpCooldown; // Reset de cooldown timer
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
