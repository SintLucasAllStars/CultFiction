using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemieController : MonoBehaviour
{
    public GameObject[] EnemyPrefab;
    public Vector3 spawnValues;

    public float enemySpawnRate;
    public float enemyMoveSpeed;

    private void Update()
    {
        StartCoroutine(SpawnRate());
        EnemyMovement();
    }

    public virtual void RandomSpawnPoints(int randomEnemies)
    {
        randomEnemies = Random.Range(0, 3);

        Vector3 spawnPositon = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        GameObject go = Instantiate(EnemyPrefab[randomEnemies], spawnPositon, spawnRotation) as GameObject;
        Destroy(go, 15);
    }

    public virtual void EnemyMovement()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - enemyMoveSpeed);
    }

    IEnumerator SpawnRate()
    {
        yield return new WaitForSeconds(enemySpawnRate);
        RandomSpawnPoints(2);
        StopAllCoroutines();
    }
}
