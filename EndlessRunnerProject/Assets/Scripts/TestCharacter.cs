using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // Optionally, initialize anything here
    }

    // Update is called once per frame
    void Update()
    {
        // Use Time.deltaTime to make movement frame-rate independent
        float hMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float vMovement = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        transform.Translate(new Vector3(hMovement, 0, vMovement));
    }

    private void OnTriggerEnter(Collider other)
    {
       spawnManager.SpawnTriggerEntered();
    }
}
