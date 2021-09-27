using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutBehaviour : MonoBehaviour
{

    public GameObject brokenCoconut;
    private bool isBroken;
    private Vector3 position;
    private Rigidbody rb;
    //public GameObject mainCamera;

    void Start()
    {
        isBroken = false;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(CoconutSpawner.mainCamera.transform.forward*220f);
        rb.AddForce(transform.up*200f);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBroken == false)
        {
            position = this.transform.position;
            Instantiate(brokenCoconut, position, Quaternion.identity);
            isBroken = true;
            Destroy(this.gameObject);
        }
    }
}
