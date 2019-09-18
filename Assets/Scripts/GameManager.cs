using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private Vector3 _playerSpawn = Vector3.zero;

    [SerializeField]
    private CameraMovement _cameraMovement;

    [SerializeField]
    private SpiderSpawner _spiderSpawner;

    [SerializeField]
    private UIManager _uiManager;

    private int _currentSpiderHighscore = 0;

    private int _currentSpiders = 0;

    private float _currentTime = 0;

    private bool _gameIsRunning = false;

    public bool GameIsRunnning
    {
        get { return _gameIsRunning; }
        set
        {
            _gameIsRunning = value;

            _uiManager.gameObject.SetActive(_gameIsRunning);

            if (_gameIsRunning)
            {
                ResetGame();
            }
            else
            {
                _uiManager.UpdateMenuScores(_currentSpiderHighscore, _currentTime);
            }
        }
    }

    public int CurrentSpiders
    {
        get { return _currentSpiders; }
        set
        {
            _currentSpiders = value;

            _uiManager.UpdateSpiderCount(_currentSpiders);

            if (_currentSpiders > _currentSpiderHighscore)
            {
                _currentSpiderHighscore = _currentSpiders;
                _uiManager.UpdateHighscoreCount(_currentSpiderHighscore);
            }
        }
    }

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
            _uiManager.UpdateTimer(_currentTime);
        }
    }

    public void StartNewRound()
    {
        GameIsRunnning = true;
        GameObject newPlayer = Instantiate(_playerPrefab, _playerSpawn, Quaternion.identity);
        _cameraMovement.SetNewTarget(newPlayer);
        StartCoroutine(_spiderSpawner.SpawnSpider());
    }

    public void KillAllFollowerSpiders()
    {
        foreach (FollowerSpider followerSpider in FindObjectsOfType<FollowerSpider>())
        {
            followerSpider.Die();
        }
    }

    private void ResetGame()
    {        
        _currentSpiderHighscore = 0;
        _currentSpiders = 0;
        _currentTime = 0;

        _uiManager.gameObject.SetActive(_gameIsRunning);
        _uiManager.ResetUI();
    }
}
