using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void OnEnable()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Level01Cutscene")
        {
            // Load "Level01"
            SceneManager.LoadScene("Level01", LoadSceneMode.Single);
        }
        else if (currentScene.name == "Level02Cutscene")
        {
            SceneManager.LoadScene("Level02", LoadSceneMode.Single);
        }
        else if (currentScene.name == "Level03Cutscene")
        {
            SceneManager.LoadScene("Level02", LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("No valid scene to load.");
        }
    }
}
