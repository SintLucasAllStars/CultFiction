using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyController : Ship
{

    [Header("Control Stats")]
    public float verticalSpeedMultiplyer;
    public float horizontalSpeedMultiplyer;
    
    [Header("Ship Parts")]
    public GameObject turboFX;

    [Header("Damage FX")]
    public GameObject[] damageStates;

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
        if (Input.GetButton("Fire1"))
        {
            transform.position += transform.forward * Time.deltaTime * dashSpeed;
            turboFX.SetActive(true);
        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * shipSpeed;
            turboFX.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RegainControl();
        }

        transform.Rotate(Input.GetAxis("Vertical") * verticalSpeedMultiplyer, 0.0f, -Input.GetAxis("Horizontal") * horizontalSpeedMultiplyer);
    }

    void CameraControl()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SwitchCamera();
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            thirdPersonCamera.SetActive(false);
            firstPersonCamera.SetActive(false);
            backCamera.SetActive(true);
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

    void SwitchCamera()
    {
        firstPerson = !firstPerson;
        if (firstPerson == true)
        {
            firstPersonCamera.SetActive(true);
            thirdPersonCamera.SetActive(false);
        }
        else
        {
            thirdPersonCamera.SetActive(true);
            firstPersonCamera.SetActive(false);
        }
    }

    void RegainControl()
    {
        //Based on broken parts maybe set a certain amount of velocity and angular velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void TakeDamage()
    {

    }

    void PlayerStates(int health)
    {
        if (health < 80 && health > 50)
        {
            damageStates[0].SetActive(true);
        }
        else if (health < 50 && health > 30)
        {
            damageStates[1].SetActive(true);
        }
        else if (health < 30)
        {
            damageStates[2].SetActive(true);
        }
    }

}