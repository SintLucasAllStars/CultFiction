using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] float spawnDelay;
    [SerializeField] GameObject prefab;
    [SerializeField] bool spawn = false;

    private void Start()
    {
        Gamemanager.ToggleSpawn += Spawn;
    }

    void Spawn(bool spawn)
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
            Instantiate(prefab, transform.position, Quaternion.identity);
            Gamemanager.instance.EnemySpawned();
        }
    }
}
