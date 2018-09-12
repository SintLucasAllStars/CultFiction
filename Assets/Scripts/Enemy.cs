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

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerScript = player.GetComponent<PlayerController>();
        }
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentMovenent = Movement.running;
    }

    // Use this for initialization
    void Spawn(Transform player, int health)
    {
        this.player = player; 
        playerScript = GetComponent<PlayerController>();
        navMesh = GetComponent<NavMeshAgent>();

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
            Debug.Log("Reset");
            canAttack = true;
            if(Vector3.Distance(transform.position, player.position) <= minDistance)
            {
                Debug.Log("Close");
                SetMovementSpeed(Movement.none);
            }
            else
            {
                Debug.Log("Far Away");
                SetMovementSpeed(Movement.running);
            }
        }

        if(Vector3.Distance(transform.position, player.position) <= minDistance)
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
            //if(currentMovenent != Movement.attack && currentMovenent != Movement.running && currentWaitTime < Time.time)
            //    SetAnimationByMovement(Movement.running);

            navMesh.SetDestination(player.position);
        }


    }

    IEnumerator Attack()
    {
        Debug.Log("Attack");
        SetMovementSpeed(Movement.attack);
        animator.Play("Attack" + (int)Random.Range(0, 2));
        SetAnimationByMovement(Movement.none);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SetMovementSpeed(Movement.walking);
        playerScript.Damage(50);

        currentWaitTime = Time.time + attackDelay;
    }

    void SetMovementSpeed(Movement movement)
    {
        currentMovenent = movement;
        navMesh.speed = (int)movement;

        SetAnimationByMovement();
    }

    void SetAnimationByMovement()
    {
        if(currentMovenent != Movement.attack)
            animator.SetInteger("Movement", (int)currentMovenent);
    }

    void SetAnimationByMovement(Movement movement)
    {
        if(movement != Movement.attack)
            animator.SetInteger("Movement", (int)movement);
    }
}