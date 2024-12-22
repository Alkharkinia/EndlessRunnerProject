using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public TMP_Text currentScoreText; // The TextMeshPro component to display the current score (end of level score)
    public TMP_Text highScoreText;    // The TextMeshPro component to display the high score

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
        currentScoreText.text = "Score: " + score.ToString();

        // Retrieve the high score from PlayerPrefs
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Default to 0 if no high score is stored

        // Update the high score if the current score is higher
        if (score > highScore)
        {
            highScore = score; // Set the new high score
            PlayerPrefs.SetInt("HighScore", highScore); // Save it to PlayerPrefs
            PlayerPrefs.Save(); // Ensure the data is written to disk immediately
        }

        // Display the high score in the second TextMeshPro object
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
