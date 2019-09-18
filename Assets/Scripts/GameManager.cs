using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private int _currentSpiderHighscore = 0;

    private float _currentTime = 0;

    private bool _gameIsRunning = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_gameIsRunning)
        {
            _currentTime += Time.deltaTime;
        }
    }
}
