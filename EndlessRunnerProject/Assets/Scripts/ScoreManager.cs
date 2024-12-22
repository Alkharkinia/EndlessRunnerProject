using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class ScoreUpdateManager : MonoBehaviour
{
    public TMP_Text currentScoreText; // The TextMeshPro component to display the current score (end of level score)
    public TMP_Text highScoreText;    // The TextMeshPro component to display the high score
    public GameObject newRecordMessage; // The GameObject to display when a new high score is achieved

    void Start()
    {
        // Display the current score and the high score when the scene starts
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        // Retrieve the current score from PlayerPrefs using the key "Score"
        int score = PlayerPrefs.GetInt("Score", 0); // Default to 0 if no score is stored

        // Display the current score in the first TextMeshPro object
        currentScoreText.text = score.ToString();

        // Retrieve the high score from PlayerPrefs
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Default to 0 if no high score is stored

        bool isNewHighScore = false; // Flag to check if a new high score is achieved

        // Update the high score if the current score is higher
        if (score > highScore)
        {
            highScore = score; // Set the new high score
            PlayerPrefs.SetInt("HighScore", highScore); // Save it to PlayerPrefs
            PlayerPrefs.Save(); // Ensure the data is written to disk immediately
            isNewHighScore = true; // Mark as new high score
        }

        // Display the high score in the second TextMeshPro object
        highScoreText.text = highScore.ToString();

        // Activate the new record message if a new high score was achieved
        if (isNewHighScore)
        {
            if (newRecordMessage != null)
            {
                newRecordMessage.SetActive(true); // Set the GameObject active
            }
        }
        else
        {
            if (newRecordMessage != null)
            {
                newRecordMessage.SetActive(false); // Keep the GameObject inactive if no new high score
            }
        }
    }
}
