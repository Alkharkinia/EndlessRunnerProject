using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;


public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public TMP_Dropdown resolutionDropdown;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("TitleScreen");
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f;          // Resume time
        gameIsPaused = false;

        resolutionDropdown.interactable = false;

        // Restore cursor lock state for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);  // Show the pause menu
        Time.timeScale = 0f;          // Pause time
        gameIsPaused = true;

        resolutionDropdown.interactable = true;

        // Unlock cursor for menu interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void resetTime()
    {
        Time.timeScale = 1f;
    }

    public void ResetAllLayerCollisions()
    {

            // Set IgnoreLayerCollision to false for the player layer and each layer
            Physics.IgnoreLayerCollision(6, 7, false);
        

        Debug.Log("All layer collisions for the player have been reset to false.");
    }

}
