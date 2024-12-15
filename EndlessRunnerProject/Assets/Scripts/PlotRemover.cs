using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotRemover : MonoBehaviour
{
    public Transform player;            // Reference to the player
    public float distanceBehindPlayer = 30f; // How far behind the player the PlotRemover will be
    public float movementSpeed = 5f;    // Speed at which the PlotRemover follows the player

    private void Update()
    {
        // Update the position of the PlotRemover behind the player
        MoveBehindPlayer();
    }

    void MoveBehindPlayer()
    {
        // Calculate target position behind the player on the Z-axis
        Vector3 targetPosition = player.position - player.forward * distanceBehindPlayer;

        // Move the PlotRemover smoothly behind the player
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);
    }

    // Called when another collider enters the PlotRemover's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided with the trigger has the "Plot" tag
        if (other.CompareTag("Plot"))
        {
            // Destroy the plot object
            Destroy(other.gameObject);
        }
    }
}

