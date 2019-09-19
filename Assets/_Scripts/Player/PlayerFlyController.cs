using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyController : Ship
{

    [Header("Control Stats")]
    public bool playerHasControl;
    public float verticalSpeedMultiplyer;
    public float horizontalSpeedMultiplyer;
    public bool gainControl;

    [Header("Ship Parts")]
    public GameObject turboFX;
    
    [Header("Camere's")]
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;
    public GameObject backCamera;

    bool firstPerson;

    [HideInInspector]
    public Rigidbody rb;

    private PlayerUI pu;
    private GameManager gm;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pu = GameObject.FindObjectOfType<PlayerUI>();
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        if (playerHasControl)
        {
            Movement();
            CameraControl();
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (health < 1)
        {
            gm.Lose();
            Debug.Log("Lose");
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.forward * Time.deltaTime * dashSpeed;
            turboFX.SetActive(true);
        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * shipSpeed;
            turboFX.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R) && gainControl == false)
        {
            gainControl = true;
        }
        if (gainControl == true)
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
        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, 0.15f);
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.15f);
        pu.DisableAlarm();
        if (rb.angularVelocity == Vector3.zero && rb.velocity == Vector3.zero)
        {
            gainControl = false;
        }
    }
}