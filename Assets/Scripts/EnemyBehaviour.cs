using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    
    [SerializeField] private int m_Health;
    public int m_Brains; //0 - 20 Determines how good the enemy is at locating treasure.

    private NavMeshAgent m_Agent;
    private Transform m_Target;
    
    // Start is called before the first frame update
    void Start()
    {
        // Getting the Nav Mesh Agent
        m_Agent = GetComponent<NavMeshAgent>();
        if (m_Agent == null)
        {
            Debug.Log("Please add Nav Mesh Agent.");
        }
        
        // Getting Target
        ChangeInterests();
        
        // Correcting Brain Settings
        if (m_Brains < 0)
        {
            m_Brains = 0;
        }
        else if (m_Brains > 20)
        {
            m_Brains = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        m_Agent.SetDestination(m_Interests[i].transform.position);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            m_Health--;
            if (m_Health < 1)
            {
                Destroy(GetComponent<NavMeshAgent>());
            }
        }
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
    
    private void OnBecameInvisible()
    {
        if (m_Health < 1)
        {
            Destroy(this.gameObject);
        }
    }
    
}
