using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public float rotX, rotY;


	// Use this for initialization
	void Start ()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
    }
	
	// Update is called once per frame
	void Update ()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * 50 * Time.deltaTime;
        rotX += mouseY * 50 * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -60f, 60f);

        transform.rotation = Quaternion.Euler(rotX, rotY, 0.0f);
    }
}
