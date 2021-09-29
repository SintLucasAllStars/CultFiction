using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public float horizontalSpeed = 1f;

    private Transform hand;

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

        hand = GameObject.Find("Hand").GetComponent<Transform>();
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

        //This handles picking up of items.
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f))
            {
                //Debug.DrawRay(cam.transform.position, cam.transform.forward * 100.0f, Color.red);
                //Debug.Log(hit.collider.name);

                var _pickup = hit.collider.gameObject;

                //If the player pickup the object, the position of it will change so it looks like it is in his hands.
                if (_pickup.name == "Pickup_Object")
                {
                    //Sets the position of the pickup object relative to the hand position.
                    _pickup.transform.position = hand.transform.position;
                    //Sets the pickup object as an child of the hand, if the camera moves the object will update.
                    _pickup.transform.SetParent(hand);
                    //Rotates the pickup object so it is right up.
                    _pickup.transform.Rotate(new Vector3(-90,0,0));
                }
            }
        }
    }
}
