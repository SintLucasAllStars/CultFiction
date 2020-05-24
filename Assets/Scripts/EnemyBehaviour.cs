using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    
    [SerializeField] private int m_Health;
    public int m_Brains; //0 - 20 Determines how good the enemy is at locating treasure.

    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_DeathGrunt;
    [SerializeField] private AudioClip m_BloodSpew;
    private NavMeshAgent m_Agent;
    private Transform m_Target;
    private ParticleSystem m_Particles;
    
    // Start is called before the first frame update
    void Start()
    {
        // Getting the Components
        m_AudioSource = GetComponent<AudioSource>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Particles = GetComponentInChildren<ParticleSystem>();
        if (m_AudioSource == null)
        {
            Debug.Log("Please add AudioSource Component.");
        }
        if (m_Agent == null)
        {
            Debug.Log("Please add Nav Mesh Agent Component.");
        }
        if (m_Particles == null)
        {
            Debug.Log("Please add Particle System Component.");
        }
        
        // Getting Target
        ChangeInterests();
        // Disable Ragdoll
        SetRigidBodyState(true);
        SetColliderState(false);
        // Disable Particle System
        m_Particles.Stop();
        
        // Setting Brains
        m_Brains += GameManager.instance.m_EnemiesKilled;
        if (m_Brains < 0)
        {
            m_Brains = 0;
        }
        else if (m_Brains > 20)
        {
            m_Brains = 20;
        }
    }

    void ChangeInterests()
    {
        int b = Random.Range(1, 20); // 0 means the enemy will never find the treasure.
        GameObject[] m_Interests;
        if(m_Brains >= b)
        {
            m_Interests = GameObject.FindGameObjectsWithTag("Treasure");
            if (m_Interests.Length < 1)
            {
                m_Interests = GameObject.FindGameObjectsWithTag("Interest");
            }
        }
        else
        {
            m_Interests = GameObject.FindGameObjectsWithTag("Interest");
        }
        int i = Random.Range(0, m_Interests.Length);
        
        // Check to see if Enemy is too close to their new interest
        float dist = Vector3.Distance(m_Interests[i].transform.position, transform.position);
        if (dist > 5)
        {
            m_Agent.SetDestination(m_Interests[i].transform.position);
        }
        else
        {
            ChangeInterests();
        }
    }

    public void Death()
    {
            GetComponent<Rigidbody>().mass = 1;
            Destroy(GetComponent<NavMeshAgent>());
            m_Particles.Play();
            m_AudioSource.clip = m_DeathGrunt;
            m_AudioSource.Play();
            
            GetComponent<Animator>().enabled = false;
            SetRigidBodyState(false);
            SetColliderState(true);
            this.gameObject.tag = "Dead";

            GameObject[] m_pos = GameObject.FindGameObjectsWithTag("Interest");
            int i = Random.Range(0, m_pos.Length);
            GameManager.instance.EnemySpawn(m_pos[i].transform.position);
            GameManager.instance.GetDeadEnemies();
    }

    void SetRigidBodyState(bool state)
    {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = state;
        }
        // Adjusting the component on the parent
        GetComponent<Rigidbody>().isKinematic = !state;
    }
    
    void SetColliderState(bool state)
    {
        Collider[] cols = GetComponentsInChildren<Collider>();
        foreach (Collider c in cols)
        {
            c.enabled = state;
        }
        // Adjusting the component on the parent
        GetComponent<Collider>().enabled = !state;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Treasure")
        {
            Transform g = other.gameObject.transform.Find("Gold");
            if (g != null)
            {
                Destroy(g.gameObject);
            }
            other.tag = "Untagged"; // This treasure has been stolen and does not need to be targeted.
        }
        
        ChangeInterests();
    }
}
