using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        // Lock cursor to center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (CameraManager.Instance.isFirstPerson && !UIManager.Instance.eventActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            LookWithMouse();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    void LookWithMouse()
    {
        // Get mouse movement
        mouseX = Input.GetAxis("Mouse X") * CameraManager.Instance.FPSCamSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * CameraManager.Instance.FPSCamSensitivity * Time.deltaTime;

        // Rotate player body horizontally
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    
}
  


