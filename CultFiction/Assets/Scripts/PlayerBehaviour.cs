﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float movementSpeed;
    public float smoothCamera;
    public float angleOffset;

    public bool isHoldingLeft;
    public bool isHoldingRight;

    public Vector3 offset;
    public Vector3 lightOffset;
    private Vector3 mousePosition;

    public GameObject playerModel;
    public GameObject hat;

    public GameObject light;
    public GameObject resetText;

    private GameObject leftHold;
    private GameObject rightHold;

    [SerializeField] private GameObject leftItem;
    private GameObject rightItem;

    private Rigidbody rb;

    public Camera mainCamera;

    private void Start()
    {
        isHoldingLeft = false;
        isHoldingRight = false;

        leftHold = GameObject.Find("LeftHold");
        rightHold = GameObject.Find("RightHold");

        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            PickUpItem();
        }

        light.transform.position = gameObject.transform.position + lightOffset;
    }

    private void FixedUpdate()
    {
        PlayerMovement();

        CameraBehaviour();

        PlayerRotateToMouse();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Arrow") || col.gameObject.CompareTag("Spike"))
        {
            gameObject.GetComponent<PlayerBehaviour>().enabled = false;

            rb.constraints = RigidbodyConstraints.None;

            hat.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            rb.AddForce(Vector3.up * Random.Range(100, 800));
            rb.AddForce(Vector3.right * Random.Range(-800, 800));
            rb.AddForce(Vector3.forward * Random.Range(-800, 800));

            rb.AddTorque(Vector3.up * Random.Range(-800, 800));
            rb.AddTorque(Vector3.right * Random.Range(-800, 800));
            rb.AddTorque(Vector3.forward * Random.Range(-800, 800));

            hat.GetComponent<Collider>().enabled = true;

            hat.transform.parent = null;

            resetText.active = true;
        }
    }

    private void PlayerMovement()
    {
        float h = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * movementSpeed;
        float z = Input.GetAxis("Vertical") * Time.fixedDeltaTime * movementSpeed;

        transform.Translate(h, 0.0f, z);
    }

    private void CameraBehaviour()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, gameObject.transform.position - offset, smoothCamera);
        mainCamera.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
    }

    private void PlayerRotateToMouse()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(mainCamera.transform.position, transform.position)));

        playerModel.transform.LookAt(mousePosition);
    }

    private void PickUpItem()
    {
        RaycastHit hit;

        if (Physics.Raycast(mousePosition + Vector3.up, Vector3.down, out hit))
        {
            if (hit.collider.CompareTag("Clickable") && Input.GetMouseButtonDown(0) && !isHoldingLeft)
            {
                leftItem = hit.collider.gameObject;

                leftItem.transform.parent = leftHold.transform;

                leftItem.transform.position = leftHold.transform.position;

                leftItem.GetComponent<Collider>().enabled = false;

                leftItem.GetComponent<Rigidbody>().useGravity = false;

                leftItem.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);

                isHoldingLeft = true;
            }
            else if (Input.GetMouseButtonDown(0) && isHoldingLeft)
            {
                leftItem.transform.parent = null;

                leftItem.GetComponent<Collider>().enabled = true;

                leftItem.GetComponent<Rigidbody>().useGravity = true;

                leftItem.GetComponent<Rigidbody>().AddForce(playerModel.transform.forward * 600.0f);

                isHoldingLeft = false;
            }

            if (hit.collider.CompareTag("Clickable") && Input.GetMouseButtonDown(1) && !isHoldingRight)
            {
                rightItem = hit.collider.gameObject;

                rightItem.transform.parent = rightHold.transform;

                rightItem.transform.position = rightHold.transform.position;

                rightItem.GetComponent<Collider>().enabled = false;

                rightItem.GetComponent<Rigidbody>().useGravity = false;

                rightItem.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);

                isHoldingRight = true;
            }
            else if (Input.GetMouseButtonDown(1) && isHoldingRight)
            {
                rightItem.transform.parent = null;

                rightItem.GetComponent<Collider>().enabled = true;

                rightItem.GetComponent<Rigidbody>().useGravity = true;

                rightItem.GetComponent<Rigidbody>().AddForce(playerModel.transform.forward * 600.0f);

                isHoldingRight = false;
            }
        }
    }
}
