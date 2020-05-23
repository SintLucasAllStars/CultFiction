using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;
    private Rigidbody2D rb;

    public float speed = 3.5f;
    private float cameraFollowSpeed = 5.0f;

    private float horizontal, vertical;
    private Vector2 moveDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(horizontal, vertical) * speed;

        //transform.Translate(moveDir); //Speed = 6f
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir;
    }

    private void LateUpdate()
    {
        Vector3 camOffset = transform.position + (-Vector3.forward * 5.0f);
        cam.transform.position = Vector3.Lerp(cam.transform.position, camOffset, Time.deltaTime * cameraFollowSpeed);
    }
}