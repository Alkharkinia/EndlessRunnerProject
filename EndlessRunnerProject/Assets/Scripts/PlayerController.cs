using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    // Movement speeds
    public float forwardSpeed = 5f;
    public float strafeSpeed = 3f;
    public float jumpForce = 7f;

    // Ground check
    private bool isGrounded = true;
    private bool isJumping = false;

    public SpawnManager spawnManager;

    void Start()
    {
        // Get Animator and Rigidbody components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        // Constant forward movement
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);

        // Reset strafe states
        bool strafingRight = false;
        bool strafingLeft = false;

        // Check for player input
        if (Input.GetKey(KeyCode.A)) // Strafe Left
        {
            rb.velocity = new Vector3(-strafeSpeed, rb.velocity.y, forwardSpeed);
            strafingLeft = true;
        }
        else if (Input.GetKey(KeyCode.D)) // Strafe Right
        {
            rb.velocity = new Vector3(strafeSpeed, rb.velocity.y, forwardSpeed);
            strafingRight = true;
        }
        else
        {
            // Maintain forward speed when not strafing
            rb.velocity = new Vector3(0, rb.velocity.y, forwardSpeed);
        }

        // Update Animator parameters
        animator.SetBool("StrafeRight", strafingRight);
        animator.SetBool("StrafeLeft", strafingLeft);

        // Update RunForward animation
        animator.SetBool("RunForward", !strafingRight && !strafingLeft && !isJumping);
    }

    void HandleJump()
    {
        // Check for jump input and ground status
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            isJumping = true;

            // Update Animator parameters
            animator.SetBool("IsJumping", true);
        }

        // Reset jump state when grounded
        if (isGrounded && isJumping)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }

    // Detect ground collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
