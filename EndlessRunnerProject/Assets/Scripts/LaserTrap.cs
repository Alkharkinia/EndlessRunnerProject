using System.Collections;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public GameObject laserObject;
    private bool isActive = true; // Tracks if the laser is currently active

    void Start()
    {
        if (laserObject == null)
        {
            Debug.LogError("Laser object not assigned!");
            return;
        }

        // Start the coroutine once in Start
        StartCoroutine(ToggleLaser());
    }

    IEnumerator ToggleLaser()
    {
        while (true) // Infinite loop to toggle laser periodically
        {
            if (isActive)
            {
                laserObject.SetActive(false);
                isActive = false;
            }
            else
            {
                laserObject.SetActive(true);
                isActive = true;
            }

            // Wait for 2 seconds before toggling again
            yield return new WaitForSeconds(2f);
        }
    }
}
