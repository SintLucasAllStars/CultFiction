using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChonkManager : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> _spawnPoints;

    [SerializeField]
    private List<Chonk> _chonkPrefabs;

    [SerializeField]
    private List<Chonk> _defaultChonk;

    private List<Chonk> _spawnedChonks = new List<Chonk>();

    private int _spawnRange = 40;
    private bool _gameStarted = false;

    private void Start()
    {
        _gameStarted = true;
        if (_gameStarted)
        {
            _gameStarted = false;
        }

        for (int i = 0; i < _defaultChonk.Count; i++)
        {
            SpawnChonk(_spawnPoints[i], _defaultChonk[i]);

        }

        for (int i = _defaultChonk.Count; i < _spawnPoints.Count; i++)
        {
            SpawnChonk(_spawnPoints[i], _chonkPrefabs[Random.Range(0, _chonkPrefabs.Count)]);

        }
    }

    private void FixedUpdate()
    {

        if (!_gameStarted)
        {
            for (int i = 0; i < _spawnedChonks.Count; i++)
            {
                if (_spawnedChonks[i].transform.position.z <= -_spawnRange)
                {
                    SpawnChonk(_spawnPoints[_spawnPoints.Count - 1], _chonkPrefabs[Random.Range(0, _chonkPrefabs.Count)]);
                    Destroy(_spawnedChonks[i].gameObject);
                    _spawnedChonks.Remove(_spawnedChonks[i]);
                    ScoreManagement.Instance.CheckScore();
                }
            }
        }
    }

    private void SpawnChonk(Vector3 spawnPoint, Chonk chonk)
    {
        Chonk _spawnedChonk = Instantiate(chonk, spawnPoint, transform.rotation);
        _spawnedChonks.Add(_spawnedChonk);
    }
}
