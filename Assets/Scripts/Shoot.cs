using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    private bool m_CanShoot;
    
    private Animator m_Animator;
    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_Shoot;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private Transform m_SpawnLoc;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
        if (m_Animator == null)
        {
            Debug.Log("Animator Component missing.");
        }
        if (m_AudioSource == null)
        {
            Debug.Log("Audio Source Component missing.");
        }

        m_AudioSource.clip = m_Shoot;
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1") && m_CanShoot == true)
        {
            m_AudioSource.Play();
            Reload();
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    EnemyBehaviour enemy = hit.collider.GetComponentInParent<EnemyBehaviour>();
                    enemy.Death();
                }
                else
                {
                    Vector3 r = new Vector3(transform.eulerAngles.x + 90, transform.eulerAngles.y - 180, 0);
                    Instantiate(m_Bullet, hit.point, Quaternion.Euler(r));
                }
            }
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
