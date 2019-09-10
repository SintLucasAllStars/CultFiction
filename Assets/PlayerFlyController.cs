using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyController : MonoBehaviour
{

    public Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Fly script added to: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.MovePosition(rb.position + transform.forward * Time.deltaTime * 10f);
        transform.position += transform.forward * Time.deltaTime * 10f;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if (Input.GetButton("Fire1"))
        {
            //rb.MovePosition(rb.position + transform.forward * Time.deltaTime * 40f);
            transform.position += transform.forward * Time.deltaTime * 40f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RegainControl();
        }

        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
        
    }

    void RegainControl()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}