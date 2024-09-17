using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform playerCamera;
    public Vector2 sensitivities;

    private Vector2 XYRotation;

    void Start()
    {
        // Hide the cursor at the start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        sensitivities.x = PlayerPrefs.GetFloat(PlayerPrefsDefinitions.SensX);
        sensitivities.y = PlayerPrefs.GetFloat(PlayerPrefsDefinitions.SensY);

        if (!PauseManager.Instance.IsPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (PauseManager.Instance.IsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        // Get mouse input for both horizontal and vertical movement
        Vector2 MouseInput = new Vector2
        {
            x = Input.GetAxis("Mouse X"),
            y = Input.GetAxis("Mouse Y")
        };

        // Update the rotation based on mouse input
        XYRotation.x -= MouseInput.y * sensitivities.y;
        XYRotation.y += MouseInput.x * sensitivities.x;

        // Clamp the vertical rotation to avoid over-rotation
        XYRotation.x = Mathf.Clamp(XYRotation.x, -70f, 70f);

        // Rotate the player horizontally
        transform.eulerAngles = new Vector3(0f, XYRotation.y, 0f);

        // Rotate the camera vertically (local rotation)
        playerCamera.localEulerAngles = new Vector3(XYRotation.x, 0f, 0f);
    }
}
