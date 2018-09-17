using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform[] bulletSpawns;

    public float attackSpeed;
    public float bulletSpeed;
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(FireRate());
        }
    }
    
    public void SpawnBullet (Transform[] spawnPositions)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPositions[i].position, Quaternion.Euler(new Vector3(90f, 0, 0)));
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * bulletSpeed;
            Destroy(bullet, 2);
        }
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(attackSpeed);
        SpawnBullet(bulletSpawns);
        StopAllCoroutines();
    }
}
