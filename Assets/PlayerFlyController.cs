using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyController : MonoBehaviour
{

    [Header("Ship Stats")]
    public float normalSpeed;
    public float dashSpeed;
    public float verticalSpeedMultiplyer;
    public float horizontalSpeedMultiplyer;

    [Header("Camere's")]
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;
    public GameObject backCamera;

    bool firstPerson;

    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Movement();
        CameraControl();
    }

    void Movement()
    {
        transform.position += transform.forward * Time.deltaTime * normalSpeed;

        if (Input.GetButton("Fire1"))
        {
            transform.position += transform.forward * Time.deltaTime * dashSpeed;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RegainControl();
        }

        transform.Rotate(Input.GetAxis("Vertical") * verticalSpeedMultiplyer, 0.0f, -Input.GetAxis("Horizontal") * horizontalSpeedMultiplyer);
    }

    void CameraControl()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (firstPerson == true)
            {
                firstPersonCamera.SetActive(false);
                backCamera.SetActive(true);
            }
            else
            {
                thirdPersonCamera.SetActive(false);
                backCamera.SetActive(true);
            }
        }
        else
        {
            if (firstPerson == true)
            {
                firstPersonCamera.SetActive(true);
                backCamera.SetActive(false);
            }
            else
            {
                thirdPersonCamera.SetActive(true);
                backCamera.SetActive(false);
            }
        }
    }

    void RegainControl()
    {
        //Based on broken parts maybe set a certain amount of velocity and angular velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}