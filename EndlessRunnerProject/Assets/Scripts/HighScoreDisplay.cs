using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class HighScoreDisplay : MonoBehaviour
{
    public TMP_Text highScoreText;  // The TextMeshPro component to display the high score

    void Start()
    {
        DisplayHighScore();  // Call the method to display the high score when the game starts
    }

    void DisplayHighScore()
    {
        // Retrieve the high score from PlayerPrefs
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Default to 0 if no high score is stored

        // Display the high score in the TextMeshPro object
        highScoreText.text = highScore.ToString();
    }
}
