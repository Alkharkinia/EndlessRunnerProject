using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorVisibility : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = true;      // Makes the cursor visible
        Cursor.lockState = CursorLockMode.None; // Ensures the cursor is not locked
    }
}
