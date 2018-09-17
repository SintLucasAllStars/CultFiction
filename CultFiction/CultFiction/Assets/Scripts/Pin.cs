using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

    Rigidbody rb;
    int add = 0;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Pin") || coll.gameObject.CompareTag("Ball"))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            if (add == 0)
            {
                GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>().pins.Add(gameObject);
                add++;
            }
        }
    }
}
