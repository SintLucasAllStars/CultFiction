using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    private bool m_CanShoot;
    
    private Animator m_Animator;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private Transform m_SpawnLoc;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        if (m_Animator == null)
        {
            Debug.Log("Animator Component missing.");
        }

        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1") && m_CanShoot == true)
        {
            Reload();
            Vector3 r = new Vector3(transform.eulerAngles.x + 90, transform.eulerAngles.y - 180, 0);
            Instantiate(m_Bullet, m_SpawnLoc.position, Quaternion.Euler(r));
        }
    }

    public void Reload()
    {
        m_CanShoot = false;
        m_Animator.Play("Bow_Reload");
    }

    public void Loaded()
    {
        m_CanShoot = true;
        m_Animator.Play("Bow_Loaded");
    }
}
