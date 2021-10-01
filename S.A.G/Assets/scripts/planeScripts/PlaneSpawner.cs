using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject Plane;
    public LevelData[] levelData;
    GameManager manager = GameManager.manager;

    private void Start()
    {
        manager.UpdatePlaneAmount(levelData[manager.dificulty].waveAmount * levelData[manager.dificulty].planesPerWave);
        StartCoroutine(WaveSpawnController(levelData[manager.dificulty].waveAmount, levelData[manager.dificulty].planesPerWave, levelData[manager.dificulty].timeBetweenWaves, levelData[manager.dificulty].timeBetweenPlanes));
    }

    private IEnumerator WaveSpawnController(int waveAmount, int planesPerWave, float timeBetweenWaves = 0, float timeBetweenPlanes = 0)
    {
        for (int i = 0; i < waveAmount; i++)
        {
            for (int j = 0; j < planesPerWave; j++)
            {
                SpawnPlane();
                yield return new WaitForSeconds(timeBetweenPlanes);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void SpawnPlane()
    {
        Instantiate(Plane, new Vector3(Random.Range(-15, 15), Random.Range(17.5f, 20), 300), Quaternion.Euler(0, 180, 0));
    }
}