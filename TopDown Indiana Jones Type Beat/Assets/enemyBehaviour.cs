using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBehaviour : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform dest;

    public float health;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = dest.position;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "LightSword")
        {
            health -= 30;
        }

        if (other.gameObject.tag == "HeavySword")
        {
            health -= 60;
        }

    }
}
