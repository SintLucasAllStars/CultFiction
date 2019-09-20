using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;

    public int time;
    public Rigidbody enemyBullet;
    public Transform[] bulletPos;

    private GameObject oldBullet;
    public int bulletCount;

    private void Start()
    {
        player = GameObject.FindWithTag ("Player").transform;
        StartCoroutine(wait());
    }

    private void Update()
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
        Debug.DrawRay(transform.position, forward, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                Mathf.Infinity) && hit.transform.CompareTag("Player"))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.yellow);
           // Debug.Log("Did Hit");


            if (time < 1)
            {
                for (int i = 0; i < bulletPos.Length; i++)
                {
                    Rigidbody clone;
                    clone = enemyBullet.GetComponent<Rigidbody>();
                    clone = Instantiate(enemyBullet, bulletPos[i].position,
                        Quaternion.LookRotation(player.transform.position));

                    clone.velocity = transform.TransformDirection(Vector3.forward * 100);
                    bulletCount++;
                    time = 0;
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
           // Debug.Log("Did not Hit");
        }
    }



    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        DestroyAll();
    }

    void DestroyAll()
    {
        StartCoroutine(wait());
        for (int i = 0; i < bulletCount; i++)
        {
            oldBullet = GameObject.Find("Bullet(Clone)");
            DestroyImmediate(oldBullet);
            bulletCount--;
        }

    }
}

