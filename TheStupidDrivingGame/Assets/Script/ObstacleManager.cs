using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float timeBetweenSpawn;
    public int amountPerSpawn;

    public Transform startpos, endpos;

    private void Start()
    {
        StartCoroutine(ObstacleSpawner());
    }

    IEnumerator ObstacleSpawner()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        for (int i = 0; i < amountPerSpawn; i++)
        {
            int randomX = (int)Random.Range(startpos.position.x, endpos.position.x);
            Vector3 randomPos = new Vector3(randomX, transform.position.y, transform.position.z);

            Instantiate(obstaclePrefab, randomPos, Quaternion.identity);
        }

        StartCoroutine(ObstacleSpawner());
    }
}
