<<<<<<< HEAD
﻿using UnityEngine.AI;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{

    [SerializeField] float minDistance;

    [SerializeField] Transform player;
    [SerializeField] PlayerController playerScript;

    bool isAttacking;

    // Use this for initialization
    void Spawn(Transform player)
    {
        this.player = player;
        playerScript = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) <= minDistance && !isAttacking)
        {
            StartCoroutine(Attack(5));
        }
    }

    IEnumerator Attack(float delayTime)
    {
        isAttacking = true;
        yield return new WaitForSeconds(delayTime);
        playerScript.Damage(100 / 3);
    }
}

=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    int health = 100;

    public void Damage(int damge)
    {
        health -= damge;
        if(health < 0)
            Destroy(gameObject);
    }
}
>>>>>>> 54ec3acd757c6dc1b2e7767c208f1dd4a56d57bb
