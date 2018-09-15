using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] float spawnDelay;
    [SerializeField] GameObject prefab;
    [SerializeField] bool spawn = false;

    Transform player;

    public void Initialize(Transform player)
    {
        this.player = player;
        Gamemanager.ToggleSpawn += StartSpawn;
    }

    void StartSpawn(bool spawn)
    {
        Debug.Log("Spawn: " + spawn);
        this.spawn = spawn;

        if(spawn)
            StartCoroutine(SpawnTimer());
    }

    IEnumerator SpawnTimer()
    {
        while(spawn)
        {
            yield return new WaitForSeconds(spawnDelay);
            Instantiate(prefab, transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity).GetComponent<Enemy>().Spawn(player, Gamemanager.instance.health);
            Gamemanager.instance.EnemySpawned();
        }
    }
}
