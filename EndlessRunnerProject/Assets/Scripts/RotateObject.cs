using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public int rotateSpeed = 1;


    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale == 0f)
        {
            return; // Skip rotation when the game is paused
        }

        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
