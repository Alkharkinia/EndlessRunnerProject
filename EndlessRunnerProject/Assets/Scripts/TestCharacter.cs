using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class TestCharacter : MonoBehaviour
{

    public  TMP_Text  scoreDisplay;
    public int disRun;
    public bool addingDis=false;

    public float movementSpeed = 10f;   // Speed at which the character moves
    public float jumpForce = 10f;        // Jump force magnitude
    public float forwardSpeed = 5f;     // Constant forward speed (vertical movement)
    public SpawnManager spawnManager;   // Reference to the SpawnManager

    public int Coins = 0;
    private float startingZ;
    public TextMeshProUGUI distanceTextTMP; // For TextMeshPro UI 
    public TextMeshProUGUI coinText;

    private Transform playerTransform;
    public GameObject enemyPrefab;

    private Rigidbody rb;               // Reference to the Rigidbody component
    private Animator animator;          // Reference to the Animator component
    private bool isGrounded = true;
    private bool isGameOver = false;

    // Animator Parameters
    private bool isStrafingRight = false;
    private bool isStrafingLeft = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();      // Get the Rigidbody component
        animator = GetComponent<Animator>(); // Get the Animator component

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
        if(!addingDis && !isGameOver){
            addingDis=true;
            StartCoroutine(AddingDis());
        }

        if (!isGameOver)
        {
            HandleMovement();
            HandleJump();
            HandleStrafing();
            UpdateDistanceUI();
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

    private void SpawnEnemies()
    {
        // Calculate spawn points relative to the player's position
        Vector3 playerPosition = playerTransform.position;
        Vector3 leftSpawnPoint = playerPosition + new Vector3(-1f, 0f, -10f); // 10 units to the left of the player
        Vector3 rightSpawnPoint = playerPosition + new Vector3(1f, 0f, -10f); // 10 units to the right of the player

        // Spawn enemies
        GameObject enemyLeft = Instantiate(enemyPrefab, leftSpawnPoint, Quaternion.identity);
        GameObject enemyRight = Instantiate(enemyPrefab, rightSpawnPoint, Quaternion.identity);

        StartCoroutine(MoveEnemy(enemyLeft, playerTransform, -1f)); // Move slightly left of the player
        StartCoroutine(MoveEnemy(enemyRight, playerTransform, 1f));  // Move slightly right of the player
    }

    private IEnumerator MoveEnemy(GameObject enemy, Transform player, float xOffset)
    {
        float duration = 1.5f; // Time to move the enemy
        float elapsed = 0f;
        Vector3 startPosition = enemy.transform.position;

        Vector3 targetPosition = new Vector3(player.position.x + xOffset, enemy.transform.position.y, player.position.z - 0.5f);

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

                SceneManager.LoadScene("GameOverScreen");
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
        }

    }

    IEnumerator AddingDis(){
        disRun += 1;
        scoreDisplay.text = "" + disRun;
        yield return new WaitForSeconds(0.25f);
        addingDis=false;
    }

}
