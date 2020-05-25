using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving : MonoBehaviour
{
    public Transform wheelTransform;

    public float wheelSensitivity = 800;
    public float speedSensitivity;
    Vector3 StartMousePos;


    public float distractionForce = 20;
    public List<Rigidbody> distractionRb = new List<Rigidbody>();


    private void Update()
    {
        transform.Translate(Vector3.left * wheelTransform.rotation.z / speedSensitivity);

        float positionX = Mathf.Clamp(transform.position.x, -20f, 20f);
        transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
        
    }

    private void FixedUpdate()
    {
        // moving object in car
        for (int i = 0; i < distractionRb.Count; i++)
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
        {
            float mouseX = (StartMousePos.x - Input.mousePosition.x) / wheelSensitivity;
            wheelTransform.Rotate(0, 0, mouseX);


            float rotationClamped = Mathf.Clamp(wheelTransform.rotation.z, -0.5f, 0.5f);
            wheelTransform.rotation = new Quaternion(0, 0, rotationClamped, wheelTransform.rotation.w);
        }
    }

    public void AddRigidbody(Rigidbody rb)
    {
        distractionRb.Add(rb);
    }

    public void RemoveRigidbody(Rigidbody rb)
    {
        distractionRb.Remove(rb);
    }
}