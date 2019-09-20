using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    float range = 8f;
    float cooldown = 0f;
    [SerializeField] float baseCooldown = 0.2f;
    GameObject currentTarget;
    [SerializeField] GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0f)
        {
            Shoot();
        }
        if (cooldown > 0f)
        {
            cooldown = cooldown - Time.deltaTime;
        }
    }


    void Shoot()
    {
        cooldown = baseCooldown;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        currentTarget = null;
        foreach (GameObject target in targets)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                currentTarget = target;
            }
        }

        if (currentTarget != null && shortestDistance <= range)
        {
            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<BulletScript>().target = currentTarget;
        }
    }
}
