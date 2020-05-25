using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// This is just a spawner for cars
/// </summary>
public class LoseManager : MonoBehaviour
{
    public GameObject carsPrefab;

    public float minMaxX;
    public float minMaxZ;

    float timeBewteenSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCars());
        timeBewteenSpawn = 3;
    }

    IEnumerator SpawnCars()
    {
        yield return new WaitForSeconds(timeBewteenSpawn);
        Vector3 randomOffset = new Vector3(Random.Range(minMaxX, minMaxX) + transform.position.x, transform.position.y, Random.Range(minMaxZ, minMaxZ) + transform.position.z);
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));

        Instantiate(carsPrefab, transform.position + randomOffset, randomRotation);
        StartCoroutine(SpawnCars());
    }
}
