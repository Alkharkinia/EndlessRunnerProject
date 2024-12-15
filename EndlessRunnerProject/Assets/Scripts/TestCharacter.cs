using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{
    public float movementSpeed = 10f;   // Speed at which the character moves
    public float jumpForce = 5f;        // Jump force magnitude
    public float forwardSpeed = 5f;     // Constant forward speed (vertical movement)
    public SpawnManager spawnManager;   // Reference to the SpawnManager

    private Rigidbody rb;               // Reference to the Rigidbody component
    private Animator animator;          // Reference to the Animator component
    private bool isGrounded = true;

    // Animator Parameters
    private bool isStrafingRight = false;
    private bool isStrafingLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();      // Get the Rigidbody component
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleStrafing();
    }

    void HandleMovement()
    {
        // Handle horizontal movement (left/right) with player input (A/D or Left/Right Arrow)
        float hMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

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

        if (transform.position.y < -2.998f) { 

            isGrounded = true;
    }

    }

    void HandleStrafing()
    {
        // Right Strafe
        if (Input.GetKeyDown(KeyCode.D)) // Replace 'D' with your desired key
        {
            isStrafingRight = true;
            isStrafingLeft = false;

            animator.SetBool("StrafeRight", true);
            animator.SetBool("StrafeLeft", false);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isStrafingRight = false;
            animator.SetBool("StrafeRight", false);
        }

        // Left Strafe
        if (Input.GetKeyDown(KeyCode.A)) // Replace 'A' with your desired key
        {
            isStrafingLeft = true;
            isStrafingRight = false;

            animator.SetBool("StrafeLeft", true);
            animator.SetBool("StrafeRight", false);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isStrafingLeft = false;
            animator.SetBool("StrafeLeft", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Notify the spawn manager when a trigger is entered
        spawnManager.SpawnTriggerEntered();
    }
}
