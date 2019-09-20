using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float sensitivity;
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse2))
        {
            float horizontal = -Input.GetAxis("Mouse X");
            float vertical = -Input.GetAxis("Mouse Y");

            Vector3 cameraMovement = new Vector3(horizontal * sensitivity, 0, vertical * sensitivity);

            transform.Translate(cameraMovement);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotateSpeed, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotateSpeed, 0);
        }
    }
}
