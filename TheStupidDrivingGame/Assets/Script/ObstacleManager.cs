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
            Instantiate(obstaclePrefab, GetRandomLaneSpawn(), Quaternion.identity);
        }

        StartCoroutine(ObstacleSpawner());
    }

    public Vector3 GetRandomLaneSpawn()
    {
        int randomX = (int)Random.Range(startpos.position.x, endpos.position.x);
        return new Vector3(randomX, transform.position.y, transform.position.z);
    }
}
