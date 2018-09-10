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
        attack = 0
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
        health -= damge;
        if(health < 0)
            Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        navMesh.SetDestination(player.position);
        if(!canAttack && currentMovenent == Movement.walking && currentWaitTime < Time.time)
        {
            Debug.Log("Reset");
            canAttack = true;
            SetMovementSpeed(Movement.running);
        }

        navMesh.SetDestination(player.position);
        if(Vector3.Distance(transform.position, player.position) <= minDistance && canAttack)
        {
            canAttack = false;
            StartCoroutine(Attack());
        }

    }

    IEnumerator Attack()
    {
        SetMovementSpeed(Movement.attack);
        animator.Play("Attack" + (int)Random.Range(0, 2));
        SetAnimationByMovement(Movement.walking);
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
        switch(currentMovenent)
        {
            case Movement.walking:
                animator.SetBool("isRunning", false);
                break;
            case Movement.running:
                animator.SetBool("isRunning", true);
                break;
        }
    }

    void SetAnimationByMovement(Movement movement)
    {
        switch(movement)
        {
            case Movement.walking:
                animator.SetBool("isRunning", false);
                break;
            case Movement.running:
                animator.SetBool("isRunning", true);
                break;
        }
    }
}