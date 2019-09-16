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
    private float _spawnIntervalMin = 1.0f;

    [SerializeField]
    private float _spawnIntervalMax = 1.0f;

    private void Start()
    {
        StartCoroutine(SpawnBall());
    }

    private IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(Random.Range(_spawnIntervalMax, _spawnIntervalMax));
        Instantiate(_bowlingBall, transform.position, Quaternion.identity);
        StartCoroutine(SpawnBall());
    }
}
