using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public float horizontalSpeed = 1f;

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private Camera cam;

    private void Start()
    {
        //Gets the camera with the 'MainCamera' tag.
        cam = Camera.main;
        //Removes and locks the cursor in the middle of the screen.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //If the mouse x/y position changes, change the camera rotation.
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * horizontalSpeed;

        //setting the correct way to change the rotation.
        yRotation += mouseX;
        xRotation -= mouseY;
        //Setting the max rotation in the x axis so the player can't rotate the camera past this point.
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //Changes the rotation of the camera.
        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }
}
