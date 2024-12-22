using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawnManager : MonoBehaviour
{
    EndlessRoadSpawner roadSpawner;
    EndlessPlotSpawner plotSpawner;

    private float elapsedTime = 0f; // Tracks elapsed game time
    private GameObject player; // Reference to the player object
    private EndlessTestCharacter playerMovement; // Reference to the player's movement script
    private float speedIncreaseInterval = 60f; // Time interval for speed increase
    private float maxSpeed = 15f; // Maximum speed cap

    // Start is called before the first frame update
    void Start()
    {
        roadSpawner = GetComponent<EndlessRoadSpawner>();
        plotSpawner = GetComponent<EndlessPlotSpawner>();

        // Find the player object and its movement script
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<EndlessTestCharacter>();
            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement script not found on the Player object!");
            }
        }
        else
        {
            Debug.LogError("Player object not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Check if it's time to increase the player's movement speed
        if (playerMovement != null && elapsedTime >= speedIncreaseInterval)
        {
            elapsedTime = 0f; // Reset the timer

            // Increase the player's speed if it's below the cap
            if (playerMovement.movementSpeed < maxSpeed)
            {
                playerMovement.movementSpeed += 1f;
                Debug.Log("Player speed increased to: " + playerMovement.movementSpeed);
            }
        }
    }

    public void SpawnTriggerEntered()
    {
        roadSpawner.MoveRoad();
        plotSpawner.SpawnPlot();
    }
}
