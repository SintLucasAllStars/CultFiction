using UnityEngine.AI;
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

