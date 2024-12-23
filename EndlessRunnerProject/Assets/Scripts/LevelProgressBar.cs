using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    public Slider progressBar; // Reference to the Slider
    public float levelDuration = 120f; // Total level duration in seconds
    public GameObject player; // Reference to the player object

    private float elapsedTime = 0f; // Tracks elapsed time
    private TestCharacter playerCollision; // Reference to the player's collision script

    void Start()
    {
        // Get the PlayerCollision component from the player object
        if (player != null)
        {
            playerCollision = player.GetComponent<TestCharacter>();
        }
    }

    void Update()
    {
        // Ensure the playerCollision reference is valid
        if (playerCollision != null && !playerCollision.hasCollided)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Update the slider value based on elapsed time
            progressBar.value = Mathf.Clamp(elapsedTime, 0, levelDuration);

            // Optional: Check if level is complete
            if (elapsedTime >= levelDuration)
            {
                CompleteLevel();
            }
        }
    }

    void CompleteLevel()
    {
        // Trigger level completion logic
        Debug.Log("Level Complete!");
    }
}
