using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    public int health;
    public State state = State.Idle;
    public Gun gun;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    public float speed;
    public float minAttackDistance, maxAttackDistance;
    private Vector2 playerLostPos;
    public Vector2 destination;

    private float enemyExtraFireRate = 0.25f;
    private float enemyDecreasedProjextilePercent = 0.6f;

    private float myDeltaTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>().transform;
        myDeltaTime = Time.time;
        destination = transform.position;
    }

    private void Update()
    {
        switch (state)
        { 
            case State.Idle:
                if(Vector2.Distance(rb.position, target.position) < maxAttackDistance)
                {
                    ChangeState(State.Attack);
                }

                break;
            case State.Attack:
                if (Vector2.Distance(rb.position, destination) < 0.1f)
                {
                    Debug.Log("Calculate new Pos");
                    Vector2 randPos;
                    randPos = rb.position;
                    randPos += new Vector2(Random.value - 0.5f, Random.value - 0.5f) * 3f;
                    destination = randPos;
                }

                moveDir = (destination - rb.position).normalized * speed;

                Vector2 attackDir = (target.position - transform.position);
                float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg - 90f;

                //Do rotation here

                if(Time.time > myDeltaTime)
                {
                    //Shoot(attackDir, angle);
                    myDeltaTime = Time.time + (gun.fireRate + enemyExtraFireRate);
                }

                break;
            case State.Search:
                break;
            case State.Dead:
                break;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir;
    }

    private void Shoot(Vector2 dir, float angle)
    {
        Vector2 firePos = rb.position + (dir.normalized * 0.55f);
        float projectileSpeed = gun.projectileSpeed * enemyDecreasedProjextilePercent;

        GameObject go = Instantiate(gun.bullet, firePos, Quaternion.Euler(0, 0, angle));
        go.GetComponent<Rigidbody2D>().AddForce(dir.normalized * projectileSpeed, ForceMode2D.Impulse);
    }

    private void ChangeState(State newState)
    {
        state = newState;
    }
}

public enum State { Idle, Attack, Search, Dead }