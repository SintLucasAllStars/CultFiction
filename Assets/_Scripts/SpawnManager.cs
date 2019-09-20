using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    int amountToSpawn;
    float spawnTimer = 1f;
    [SerializeField] float baseSpawnTimer = 1f;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        amountToSpawn = 1000000;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer = spawnTimer - Time.deltaTime;
        if (spawnTimer <= 0f && amountToSpawn > 0)
        {
            Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
            spawnTimer = baseSpawnTimer;
            amountToSpawn--;
        }
    }
}
