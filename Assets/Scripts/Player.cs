using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject bullet;

    public float speed = 3.5f;
    public float projectileSpeed = 5.0f;
    private float cameraFollowSpeed = 5.0f;

    private float horizontal, vertical;
    private Vector2 moveDir;
    private Vector2 mousePos;
    private Vector2 lookDir;
    private float angle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(horizontal, vertical) * speed;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        lookDir = mousePos - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        Rotate();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir;
    }

    private void Rotate()
    {
        int parameterValue = GetRotation(angle);
        anim.SetFloat("lookDir", parameterValue);
    }

    private void Shoot()
    {
        Vector2 firePos = rb.position + (lookDir.normalized * 0.55f);

        GameObject go = Instantiate(bullet, firePos, Quaternion.Euler(0, 0, angle));
        go.GetComponent<Rigidbody2D>().AddForce(lookDir * projectileSpeed, ForceMode2D.Impulse);
    }

    private void LateUpdate()
    {
        Vector3 camOffset = transform.position + (-Vector3.forward * 5.0f);
        cam.transform.position = Vector3.Lerp(cam.transform.position, camOffset, Time.deltaTime * cameraFollowSpeed);
    }

    private int GetRotation(float angle)
    {
        if (angle < 23 && angle >= -23)
        {
            //North
            return 1;
        }
        else if (angle < -23 && angle >= -68)
        {
            //North East
            return 2;
        }
        else if (angle < -68 && angle >= -113)
        {
            //East
            return 3;
        }
        else if (angle < -113 && angle >= -158)
        {
            //South East
            return 4;
        }
        else if (angle < -158 && angle >= -203)
        {
            //South
            return 5;
        }
        else if (angle < -203 && angle >= -248)
        {
            //South West
            return 6;
        }
        else if (angle < -248 || angle >= 68)
        {
            //West
            return 7;
        }
        else if (angle < 68 && angle >= 23)
        {
            //North West
            return 8;
        }
        else
        {
            Debug.LogError("Angle: " + angle + " none of the directions compatible!!");
            return 1;
        }
    }
}