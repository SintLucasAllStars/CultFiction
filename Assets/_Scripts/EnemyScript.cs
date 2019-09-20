using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    int health = 20;
    [SerializeField] private Transform[] waypoints;
    int currentWaypoint = 0;
    [SerializeField] float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, currentSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.05f)
        {
            currentWaypoint++;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health = health - 5;
            if (health <= 0){
                Destroy(gameObject);
            }
        }
    }
}
