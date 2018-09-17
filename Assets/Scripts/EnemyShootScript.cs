using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Transform target;

    public float pauseTime;
    public int numberOfProjectiles;
    public float projectileSpeed;
    public int shootStyle;
    public float startTimeBtwProjectiles;

    private Vector3 startPoint;
    private float timeBtwProjectiles;
    private const float radius = 1F; 

    void Update()
    {
        startPoint = transform.position;

        switch (shootStyle)
        {
            case 1:
                StartCoroutine(EnemyFirerate());
                break;

            case 2:
                LineairShooting(numberOfProjectiles);
                break;
        }
    }

    private void SpawnProjectile(int _numberOfProjectiles)
    {
        float angleStep = 360f / _numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= _numberOfProjectiles - 1; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(ProjectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);

            Destroy(tmpObj, 10f);

            angle += angleStep;
        }
    }

    public void LineairShooting(int _numerOfProjectiles)
    {
        for (int i = 0; i < _numerOfProjectiles -1; i++)
        {
            if (timeBtwProjectiles <= 0)
            {
                GameObject tmpObj = Instantiate(ProjectilePrefab, startPoint, Quaternion.identity);
                transform.LookAt(target);
                tmpObj.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
                timeBtwProjectiles = startTimeBtwProjectiles;
                Destroy(tmpObj, 5);
            }
            else
            {
                timeBtwProjectiles -= Time.deltaTime;
            }
        } 
    }

    IEnumerator EnemyFirerate()
    {
        yield return new WaitForSeconds(pauseTime);
        SpawnProjectile(numberOfProjectiles);
        StopAllCoroutines();
    }
}
