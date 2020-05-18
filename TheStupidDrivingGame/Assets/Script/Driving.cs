using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving : MonoBehaviour
{
    public Transform wheelTransform;

    public float sensitivity = 800;
    public float speedSensitivity;
    Vector3 StartMousePos;


    public float distractionForce = 20;
    Rigidbody[] distractionRb;

    private void Start()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Distraction");
        distractionRb = new Rigidbody[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            distractionRb[i] = temp[i].GetComponent<Rigidbody>();
        }
        
    }

    private void Update()
    {
        transform.Translate(Vector3.left * wheelTransform.rotation.z / speedSensitivity);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < distractionRb.Length; i++)
        {
            distractionRb[i].AddForce(Vector3.left * wheelTransform.rotation.z * distractionForce);
        }
    }

    private void OnMouseDown()
    {
        StartMousePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        float mouseX = (StartMousePos.x - Input.mousePosition.x) / sensitivity;
        wheelTransform.Rotate(0, 0, mouseX);
    }
}