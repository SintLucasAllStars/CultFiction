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
        rb.AddForce(CoconutSpawner.mainCamera.transform.forward*Random.Range(160f, 280f));
        rb.AddForce(transform.up*Random.Range(140f, 220f));
        rb.AddForce(transform.right *Random.Range(-50f,50f));

    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && isBroken == false)
        {
            position = this.transform.position;
            Instantiate(brokenCoconut, position, Quaternion.identity);
            isBroken = true;
            Destroy(this.gameObject);
        }
    }
}
