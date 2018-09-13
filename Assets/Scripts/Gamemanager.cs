using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public delegate void SpawnEvent(bool spawn);
    public static event SpawnEvent ToggleSpawn;

    public static Gamemanager instance;

    [SerializeField] int currentEnemySpawned;
    [SerializeField] float multiplier;
    [SerializeField] float spawnDelay;

    int enemyLeftToSpawn;
    int currentEnemysAlive;

    int score;

    private void Awake()
    {
        ToggleSpawn = null;
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        SpawnEnemys();
    }

    public void EnemyDied()
    {
        currentEnemysAlive--;
        score += 100;
        if(currentEnemysAlive <= 0 && enemyLeftToSpawn <= 0)
            SpawnEnemys();
    }

    public void EnemySpawned()
    {
        enemyLeftToSpawn--;
        currentEnemysAlive++;
        if(enemyLeftToSpawn <= 0)
            if(ToggleSpawn != null)
                ToggleSpawn(false);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int points)
    {
        score += points;
    }

    void SpawnEnemys()
    {
        StartCoroutine(Spawn());
        currentEnemySpawned = Mathf.CeilToInt(currentEnemySpawned * multiplier);
        enemyLeftToSpawn = currentEnemySpawned;
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        Debug.Log("Let the games begin");
        if(ToggleSpawn != null)
            ToggleSpawn(true);
    }
}
