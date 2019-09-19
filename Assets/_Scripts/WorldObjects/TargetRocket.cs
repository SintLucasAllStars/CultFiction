using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRocket : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    
    [SerializeField]
    [Range(0, 2)]
    private float rotationSpeed;
    
    [SerializeField] private float focusDistance = 5;
    public int damage;
    public Transform target;
    public Transform explosionSpawn;
    public GameObject explosion;
    public bool enemyRocket;
    private bool isFollowingTarget = true;
    private Vector3 tempVector;

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < focusDistance)
        {
            isFollowingTarget = false;
        }

        Vector3 targetDirection = target.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward , targetDirection, rotationSpeed * Time.deltaTime, 0.0F);

        MoveForward(Time.deltaTime);

        if (isFollowingTarget)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }
    
    private void MoveForward(float rate)
    {
        transform.Translate(Vector3.forward * rate * speed, Space.Self);
    }

    void OnTriggerEnter(Collider col)
    {
        if (enemyRocket)
        {
            if (col.CompareTag("Shield"))
            {
                return;
            }
            else if(col.CompareTag("Player"))
            {
                col.GetComponent<PlayerFlyController>().TakeDamage(damage);
                Explode();
            }
            else if (col.CompareTag("Enemy") == false) // To Prevent Exploding on spawm
            {
                Explode();
            }
        }
        if (!enemyRocket)
        {
            if (col.CompareTag("Shield"))
            {
                Explode();
            }
            else if (col.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyBehaviour>().TakeDamage(damage);
                Explode();
            }
            else if (col.CompareTag("Player") == false) // To Prevent Exploding on spawm
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Instantiate(explosion, explosionSpawn.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
