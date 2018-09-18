using UnityEngine.AI;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    int health = 100;
    [SerializeField] float minDistance = 1;

    Transform player;
    PlayerController playerScript;

    bool canAttack = true;

    Animator animator;
    NavMeshAgent navMesh;

    public enum Movement
    {
        running = 3,
        walking = 1,
        attack = 0,
        none = 2,
        dead = 100
    }
    public Movement currentMovenent = Movement.running;


    [SerializeField] float attackDelay;
    float currentWaitTime;

    // Use this for initialization
    public void Spawn(Transform player, int health)
    {
        this.player = player;
        playerScript = player.GetComponent<PlayerController>();
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentMovenent = Movement.running;

        navMesh.SetDestination(player.position);

    }

    public void Damage(int damge)
    {
        if(currentMovenent == Movement.dead)
            return;

        health -= damge;
        if(health <= 0)
        {
            Debug.Log("RIP");
            animator.Play("Death");
            Destroy(gameObject, 10);
            navMesh.speed = 0;
            currentMovenent = Movement.dead;
            Gamemanager.instance.EnemyDied();

            BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();
            for(int i = 0; i < colliders.Length; i++)
            {
                Destroy(colliders[i]);
            }
        }
        else
        {
            animator.Play("Hit");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(currentMovenent == Movement.dead)
            return;

        if(!canAttack && currentMovenent == Movement.walking && currentWaitTime < Time.time)
        {
            canAttack = true;
            if(CheckDistance())
            {
                SetMovementSpeed(Movement.none);
            }
            else
            {
                SetMovementSpeed(Movement.running);
            }
        }

        if(CheckDistance())
        {
            if(canAttack)
            {
                canAttack = false;
                StartCoroutine(Attack());
            }
            if(currentMovenent != Movement.attack || currentMovenent != Movement.none)
                SetAnimationByMovement(Movement.none);

            navMesh.SetDestination(transform.position);

        }
        else
        {
            navMesh.SetDestination(player.position);
        }
    }

    IEnumerator Attack()
    {
        SetMovementSpeed(Movement.attack);
        animator.Play("Attack" + (int)Random.Range(0, 2));

        const float attackLength = 0.08f;
        float lengthAnimation = animator.GetCurrentAnimatorStateInfo(0).length - attackLength;

        SetAnimationByMovement(Movement.none);

        transform.LookAt(player);

        yield return new WaitForSeconds(attackLength);

        if(CheckDistance())
            playerScript.Damage(30);

        yield return new WaitForSeconds(lengthAnimation);

        SetMovementSpeed(Movement.walking);

        currentWaitTime = Time.time + attackDelay;
    }

    void SetMovementSpeed(Movement movement)
    {

        if(currentMovenent == Movement.dead)
            return;

        currentMovenent = movement;
        navMesh.speed = (int)movement;

        SetAnimationByMovement();
    }

    void SetAnimationByMovement()
    {
        if(currentMovenent != Movement.attack && currentMovenent != Movement.dead)
            animator.SetInteger("Movement", (int)currentMovenent);
    }

    void SetAnimationByMovement(Movement movement)
    {
        if(movement != Movement.attack && movement != Movement.dead)
            animator.SetInteger("Movement", (int)movement);
    }

    bool CheckDistance()
    {
        return Vector3.Distance(transform.position, player.position) <= minDistance;
    }
}