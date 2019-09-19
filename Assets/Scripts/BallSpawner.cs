using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _bowlingBall;

    [SerializeField]
    private List<Material> _materials = new List<Material>();

    [SerializeField]
    private int _spawnIntervalMin = 1;

    [SerializeField]
    private int _spawnIntervalMax = 7;

    private void Start()
    {
        StartCoroutine(SpawnBall());
    }

    private IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(Random.Range(_spawnIntervalMin, _spawnIntervalMax));
        Instantiate(_bowlingBall, transform.position, Quaternion.identity);
        StartCoroutine(SpawnBall());
    }
}
