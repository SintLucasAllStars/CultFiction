using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int m_Health;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            m_Health--;
            if (m_Health < 1)
            {
                Debug.Log("This guy died.");
            }
        }
    }

    private void OnBecameInvisible()
    {
        if (m_Health < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
