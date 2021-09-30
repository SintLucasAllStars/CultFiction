using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    public Transform player;
    private float moveSpeed = 3;

    private int minDist = 8;
    private int maxDist = 20;
    private RaycastHit hit;
    private bool canAttack;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    { 
        transform.LookAt(player);

        //Debug.DrawRay(transform.position, player.transform.position - transform.position);

        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            //Debug.Log("hitdist: " + hit.distance);

            if (hit.distance > minDist && hit.distance < maxDist)
            {
                //move to player
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            if (hit.distance < minDist + 1 && hit.distance > minDist +1)
            {
                Debug.Log("I can attack you");
                AttackPlayer();
            }

        }

        Debug.Log(canAttack);
    }

    private void AttackPlayer()
    {
        
    }
}
