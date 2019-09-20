using UnityEngine;

public class GameManager
{
    private static GameManager _gameManager;

    private int _highScore;
    private int _lastScore;

    private GameManager()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", -1);
        _lastScore = -1;
    }

    public static GameManager GetGameManager()
    {
        if (_gameManager == null)
        {
            _gameManager = new GameManager();
        }
        return _gameManager;
    }

    public void EndGame(int score)
    {
        _lastScore = score;
        if (score > _highScore)
        {
            _highScore = score;
            PlayerPrefs.SetInt("HighScore", _highScore);
            PlayerPrefs.Save();
        }
    }

    public bool HasJustFinished()
    {
        return _lastScore != -1;
    }

    public bool HasPlayedBefore()
    {
        return _highScore != -1;
    }

    public int GetLastScore()
    {
        return _lastScore;
    }

    public int GetHighScore()
    {
        return _highScore;
    }
    
}