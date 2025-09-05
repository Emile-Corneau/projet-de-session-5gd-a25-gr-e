using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCamera : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    public float mouseSensitivity = 2f;
    public float yRotationLimit = 360f; // Prevents camera flipping
    public float xRotationLimit = 360f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY = Mathf.Clamp(rotationY, -yRotationLimit, yRotationLimit);
        rotationX = Mathf.Clamp(rotationX, -xRotationLimit, xRotationLimit);

        // Apply horizontal rotation to the player/parent object
        transform.localRotation = Quaternion.Euler(0f, rotationX, 0f);

        // Apply vertical rotation to the camera itself
        m_Camera.transform.localRotation = Quaternion.Euler(-rotationY, 0f, 0f);
    }
}

