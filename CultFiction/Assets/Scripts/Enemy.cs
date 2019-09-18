using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject explosion;
    public float speed;
    public int health;
    public NavMeshAgent nav;
    public List<Transform> endpoints;
    
    void Start()
    {
        endpoints = new List<Transform>();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Endpoints"))
        {
            endpoints.Add(item.transform);
        }
        nav.destination = endpoints[Random.Range(0, endpoints.Count)].position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(nav.destination);
        if (Vector3.Distance(nav.destination, gameObject.transform.position) <= 1)
        {
            nav.isStopped = true;
        }
    }
    
    public void GetHit(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Gamemanager.instance.UpdatePoints(1);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("GolfBall"))
        {
            //Camera.main.GetComponent<AudioSource>().Play();
            Instantiate(explosion, collision.contacts[0].point, Quaternion.identity);
            GetHit(10);
            Destroy(collision.gameObject);
        }
    }
}
