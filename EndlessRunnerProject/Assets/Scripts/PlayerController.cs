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
        // Handle horizontal movement (left/right) with player input (A/D or Left/Right Arrow)
        float hMovement = Input.GetAxis("Horizontal") * forwardSpeed * Time.deltaTime;

        // Apply constant forward movement along the z-axis (vertical movement in Unity's world space)
        float vMovement = forwardSpeed * Time.deltaTime; // Constant speed on the z-axis

        // Move the character based on input and constant forward speed
        transform.Translate(new Vector3(hMovement, 0, vMovement));
    }

    void HandleJump()
    {
        // Check for jump input and if the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Apply upward force for jumping
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Set the Animator trigger for the jump animation
            animator.SetTrigger("Jump");

            isGrounded = false;

        }

        if (transform.position.y < -2.998f)
        {

            isGrounded = true;
        }
    }

    // Detect ground collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
           
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
        Debug.Log("Collision with obstacle detected!");

        // Play falling animation
        animator.SetTrigger("StumbleBackwards");

        // Stop player movement
        forwardSpeed = 0f;
        strafeSpeed = 0f;

        // Freeze Rigidbody movement
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        

        // Disable further input
        enabled = false;
    }


    }
}
