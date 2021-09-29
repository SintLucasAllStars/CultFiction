using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject Plane;

    private void Start()
    {
       StartCoroutine(WaveSpawnController(4 ,3, 5, 1));
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
        Instantiate(Plane, new Vector3(Random.Range(-15, 15), Random.Range(17.5f, 20), 200), Quaternion.Euler(0, 180, 0));
    }
}
