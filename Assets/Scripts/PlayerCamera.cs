using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    float smooth = 5.0f;
    public float tiltAnglex = 15.0f;
    public float tiltAngley = 5f;

    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public Transform player;
    public Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAnglex;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngley;

        Quaternion target = Quaternion.Euler(-tiltAroundX, 0, tiltAroundZ);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        transform.position = player.position + offset;
    }
}
