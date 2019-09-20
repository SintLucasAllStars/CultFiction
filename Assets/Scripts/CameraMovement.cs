using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraDistOffsetz = 1;
    public float cameraDistOffsety = 1;
    public float cameraDistOffsetx = 0;
    public Camera mainCamera;
    public GameObject player;
    public int rotateSpeed = 25;
    public Transform playerr;
    public Vector3 offset;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;



    // Use this for initialization
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}