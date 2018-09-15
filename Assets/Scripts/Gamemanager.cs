using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public delegate void SpawnEvent(bool spawn);
    public static event SpawnEvent ToggleSpawn;

    public static Gamemanager instance;

    [Header("Enemys")]
    [SerializeField] int currentEnemySpawned;
    [SerializeField] float multiplier;

    [Header("Enemy Health")]
    public int health;
    [SerializeField] float healtMultiplier;
 
    [SerializeField] float spawnDelay;

    int enemyLeftToSpawn;
    int currentEnemysAlive;

    int score = 500;
    int round = 1;

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

    public int GetRound()
    {
        return round;
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
        health = Mathf.CeilToInt(health * healtMultiplier);
        yield return new WaitForSeconds(spawnDelay);
        Debug.Log("Let the game begin");
        if(ToggleSpawn != null)
            ToggleSpawn(true);
        round++;
    }
}
