using System.Collections;
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
    private Vector3 mousePosition;

    public GameObject playerModel;

    private GameObject leftHold;
    private GameObject rightHold;

    public Camera mainCamera;

    private void Start()
    {
        isHoldingLeft = false;
        isHoldingRight = false;
    }

    private void Update()
    {
        PickUpItem();
    }

    private void FixedUpdate()
    {
        PlayerMovement();

        CameraBehaviour();

        PlayerRotateToMouse();
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

        if (Physics.Raycast(mousePosition, transform.up, out hit))
        {
            if (hit.collider.CompareTag("Clickable") && Input.GetMouseButtonDown(0))
            {
                Debug.Log("HIT_Left");

                isHoldingLeft = true;

                hit.collider.transform.parent = leftHold.transform;

                hit.collider.transform.position = leftHold.transform.position;

                hit.collider.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                hit.collider.enabled = false;
            }

            if (hit.collider.CompareTag("Clickable") && Input.GetMouseButtonDown(1))
            {
                Debug.Log("HIT_Right");

                isHoldingRight = true;

                hit.collider.transform.parent = rightHold.transform;

                hit.collider.transform.position = rightHold.transform.position;

                hit.collider.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                hit.collider.enabled = false;
            }

            if (Input.GetMouseButtonDown(0) && isHoldingRight)
            {
                isHoldingLeft = false;

                hit.collider.enabled = true;
            }

            if (Input.GetMouseButtonDown(1) && isHoldingRight)
            {
                isHoldingRight = false;

                hit.collider.enabled = true;
            }
        }
    }
}
