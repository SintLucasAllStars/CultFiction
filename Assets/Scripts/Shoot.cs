using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private Transform m_SpawnLoc;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonUp("Fire1"))
        {
            Vector3 r = new Vector3(transform.eulerAngles.x + 90, transform.eulerAngles.y, 0);
            
            Instantiate(m_Bullet, m_SpawnLoc.position, Quaternion.Euler(r));
        }
        
    }
}
