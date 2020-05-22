using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb = null;
    private bool gunEquipt = true;

    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private Gun gun = null;
    [SerializeField]
    private GameObject Hand = null;
    [SerializeField]
    private GameObject hips = null;
    [SerializeField]
    private Transform GunLoc= null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
       
    }

    private void Update()
    {
        if (gunEquipt && Input.GetKeyDown(KeyCode.Space))
        {
            Hand.GetComponent<Rigidbody>().AddForce(transform.up + -transform.right * 100);
            gun.Fire();
        }
    }

    void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        rb.AddForce(hips.transform.right * vAxis * speed);
        transform.eulerAngles += transform.up * hAxis * 2;

    }

}
