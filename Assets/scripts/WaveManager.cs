using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int currentWave = 0;
    private int enemiesInWave;
    private int enemiestoSpawn;

    private WaveUI ui;
    private Timer timer;
    private GameManager manager;

    private List<Transform> spawners = new List<Transform>();

    public float spawnDelay;
    public float roundDelay;
    public int enemyIncreaseFactor;

    public GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<GameManager>();
        timer = GetComponent<Timer>();
        ui = GetComponent<WaveUI>();

        ui.HideUI();

        foreach (Transform child in transform)
        {
            spawners.Add(child);
        }
    }

    public void StartWave()
    {
        currentWave += 1;

        enemiesInWave = enemyIncreaseFactor * currentWave;
        enemiestoSpawn = enemiesInWave;

        ui.UpdateUI(currentWave, enemiesInWave);

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < enemiestoSpawn; i++)
        {
           Instantiate(enemy, GetRandomSpawner(), Quaternion.identity);

           yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void EnemyDied()
    {
        enemiesInWave -= 1;

        ui.UpdateUI(currentWave, enemiesInWave);

        if (enemiesInWave == 0)
        {
            EndWave();
        }
    }

    public void EndWave()
    {
        ui.HideUI();
        timer.StartTimer(roundDelay);
    }

    public Vector3 GetRandomSpawner()
    {
        return spawners[Random.Range(0, spawners.Count)].position;
    }
}
