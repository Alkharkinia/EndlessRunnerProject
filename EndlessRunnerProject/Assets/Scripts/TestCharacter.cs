using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class TestCharacter : MonoBehaviour
{
    public AudioSource coinSFX;
    public  TMP_Text  scoreDisplay;
    public int disRun;
    public bool addingDis=false;

    public float movementSpeed = 10f;   // Speed at which the character moves
    public float jumpForce = 0f;        // Jump force magnitude
    public float forwardSpeed = 5f;     // Constant forward speed (vertical movement)
    public SpawnManager spawnManager;   // Reference to the SpawnManager

    public int Coins = 0;
    private float startingZ;
    public TextMeshProUGUI distanceTextTMP; // For TextMeshPro UI 
    public TextMeshProUGUI coinText;

    private Transform playerTransform;
    public GameObject enemyPrefab;

    private int playerLayer = 6; // Default player layer
    private int obstacleLayer = 7; // Layer for obstacles
    public GameObject invincibilityGlow;  // Reference to the light object that indicates invincibility
    public GameObject invincibilityObject;

    public TMP_Text countdownText;
    public float invincibilityDuration = 10f;  // Duration for invincibility (in seconds)

    private Rigidbody rb;               // Reference to the Rigidbody component
    private Animator animator;          // Reference to the Animator component

    private bool isGrounded;
    private bool isInvincible = false;

    private float survivalTime = 120f;
    private float gameTime = 0f;
    private bool isGameOver = false;
    private bool victoryTriggered = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();      // Get the Rigidbody component
        animator = GetComponent<Animator>(); // Get the Animator component

        invincibilityGlow.SetActive(false);
        invincibilityObject.SetActive(false);


        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found. Ensure the player has the correct tag.");
        }

        startingZ = playerTransform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isGameOver && !victoryTriggered)
        {
            gameTime += Time.deltaTime;
            //Debug.Log(gameTime);

            // Check if the player survived for the required time
            if (gameTime >= survivalTime)
            {
                StartCoroutine(TriggerVictoryAnimation());
            }
        }

        if (!addingDis && !isGameOver && !victoryTriggered)
        {
            addingDis=true;
            StartCoroutine(AddingDis());
        }

        if (!isGameOver && !victoryTriggered)
        {
            HandleMovement();
            HandleJump();
            HandleStrafing();
            UpdateDistanceUI();
        }

        if (Coins % 50 == 0 && Coins != 0)
        {
            StartCoroutine(Invincibility());
        }
    }

    IEnumerator TriggerVictoryAnimation()
    {
        if (animator != null)
        {
            victoryTriggered = true; // Ensure this happens only once
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<TestCharacter>().forwardSpeed = 0f;
            animator.SetBool("hasWon", true); // Trigger the victory animation
            Debug.Log("Victory animation played!");
        }
        else
        {
            Debug.LogWarning("Player Animator is not assigned!");
        }

        yield return new WaitForSeconds(2.5f);

        if (SceneManager.GetActiveScene().name == "Level01")
        {
            SceneManager.LoadScene("Level01Complete");
        }
        else if (SceneManager.GetActiveScene().name == "Level02")
        {
            SceneManager.LoadScene("Level02Complete");
        }
        else if (SceneManager.GetActiveScene().name == "Level03")
        {
            SceneManager.LoadScene("Level03Complete");
        }
        else if (SceneManager.GetActiveScene().name == "LevelEndless")
        {
            SceneManager.LoadScene("LevelEndlessComplete");
        }
        else
        {
            Debug.LogError("No Valid Scene to Load.");
        }
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
            //Debug.Log("Player Jumped.");

            // Set the Animator trigger for the jump animation
            animator.SetTrigger("Jump");

        }

    }

    void HandleStrafing()
    {
        // Right Strafe
        if (Input.GetKeyDown(KeyCode.D)) // Replace 'D' with your desired key
        {

            animator.SetBool("StrafeRight", true);
            animator.SetBool("StrafeLeft", false);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("StrafeRight", false);
        }

        // Left Strafe
        if (Input.GetKeyDown(KeyCode.A)) // Replace 'A' with your desired key
        {

            animator.SetBool("StrafeLeft", true);
            animator.SetBool("StrafeRight", false);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("StrafeLeft", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<TestCharacter>().forwardSpeed = 0f;
            animator.SetBool("Collision", true); 

            SpawnEnemies();

            
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Assuming the ground has the "Ground" tag
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void SpawnEnemies()
    {
        // Calculate spawn points relative to the player's position
        Vector3 playerPosition = playerTransform.position;
        Vector3 leftSpawnPoint = new Vector3(-1f + playerPosition.x, -3f, -20f + playerPosition.z); // 10 units to the left of the player
        Vector3 rightSpawnPoint = new Vector3(1f + playerPosition.x, -3f, -20f + playerPosition.z); // 10 units to the right of the player

        // Spawn enemies
        GameObject enemyLeft = Instantiate(enemyPrefab, leftSpawnPoint, Quaternion.identity);
        GameObject enemyRight = Instantiate(enemyPrefab, rightSpawnPoint, Quaternion.identity);

        StartCoroutine(MoveEnemy(enemyLeft, playerTransform, -1f)); // Move slightly left of the player
        StartCoroutine(MoveEnemy(enemyRight, playerTransform, 1f));  // Move slightly right of the player
    }

    private IEnumerator MoveEnemy(GameObject enemy, Transform player, float xOffset)
    {
        float duration = 2.5f; // Time to move the enemy
        float elapsed = 0f;
        Vector3 startPosition = enemy.transform.position;

        Vector3 targetPosition = new Vector3(player.position.x + xOffset, enemy.transform.position.y, player.position.z - 2.5f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // Use unscaled time
            enemy.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }

        if (elapsed >= duration)
        {
            // Set the "hasArrived" flag to true to trigger the next animation
            Animator animator = enemy.GetComponent<Animator>(); // Get the Animator component
            if (animator != null)
            {
                animator.SetBool("hasArrived", true); // Trigger the animation change
                yield return new WaitForSeconds(2.5f);

                if (SceneManager.GetActiveScene().name == "Level01")
                {
                    SceneManager.LoadScene("GameOverScreen01");
                }
                else if (SceneManager.GetActiveScene().name == "Level02")
                {
                    SceneManager.LoadScene("GameOverScreen02");
                }
                else if (SceneManager.GetActiveScene().name == "Level03")
                {
                    SceneManager.LoadScene("GameOverScreen03");
                }
                else if (SceneManager.GetActiveScene().name == "LevelEndless")
                {
                    SceneManager.LoadScene("GameOverScreen03");
                }
                else
                {
                    Debug.LogError("No Valid Scene to Load.");
                }


            }

        }

    }

    private void UpdateDistanceUI()
    {
        if (playerTransform != null)
        {
            // Calculate the Z-axis distance from the starting position
            int distance = Mathf.Abs((int)playerTransform.position.z - (int)startingZ);

            if (distanceTextTMP != null)
                distanceTextTMP.text = $"Distance: {distance}m";
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        
        if (other.CompareTag("SpawnTrigger"))
        {
            spawnManager.SpawnTriggerEntered();

        }
        
        if (other.transform.tag == "Coin")
        {
            
            Coins++;
            coinText.text = "Coin: " + Coins.ToString();
            Destroy(other.gameObject);
             coinSFX.Play();
        }

    }

    IEnumerator Invincibility()
    {
        isInvincible = true;

        Physics.IgnoreLayerCollision(playerLayer, obstacleLayer, true);

        invincibilityGlow.SetActive(true);
        invincibilityObject.SetActive(true);

        StartCoroutine(InvincibilityCountdown());

        // Wait for invincibility duration
        yield return new WaitForSeconds(10f);

        // End invincibility
        isInvincible = false;

        Physics.IgnoreLayerCollision(playerLayer, obstacleLayer, false);

        invincibilityGlow.SetActive(false);
    }

    private IEnumerator InvincibilityCountdown()
    {
        float timeRemaining = invincibilityDuration;
        

        while (timeRemaining > 0)
        {
            // Update the countdown text every frame with an integer value
            countdownText.text = "Invincibility Activated: " + Mathf.RoundToInt(timeRemaining) + "s";
            timeRemaining -= Time.deltaTime;

            yield return null; // Wait until the next frame
        }

        // Once the countdown is over, turn off the light and clear the text
        invincibilityObject.SetActive(false); // Clear the text when invincibility ends
    }

    IEnumerator AddingDis(){
        disRun += 1;
        scoreDisplay.text = "" + disRun;
        yield return new WaitForSeconds(0.25f);
        addingDis=false;
    }

}
