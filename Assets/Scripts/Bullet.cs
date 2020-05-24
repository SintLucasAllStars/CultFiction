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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("Hit a wall.");
            transform.parent = other.gameObject.transform;
            Rigidbody m_rb = GetComponent<Rigidbody>();
            m_rb.isKinematic = true;
            m_Active = false;
            gameObject.tag = "Untagged";
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
