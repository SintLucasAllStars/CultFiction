using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private bool m_Active;
    [SerializeField] private int m_Power;
    [SerializeField] private int m_Speed;
    [SerializeField] private float m_MaxDistance;

    // Start is called before the first frame update
    void Start()
    {
        m_Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Active == true)
        {
            transform.Translate(Vector3.up * (m_Speed * 10) * Time.deltaTime);
        }
    }

    /*private void OnCollisionEnter(Collision other)
    {
        transform.parent = other.gameObject.transform;
        m_Active = false;
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        Collider bc = gameObject.GetComponent<Collider>();
        if (rb != null && bc != null)
        {
            rb.AddForce(Vector3.up * m_Power * 100);
            Destroy(bc);
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }*/

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
