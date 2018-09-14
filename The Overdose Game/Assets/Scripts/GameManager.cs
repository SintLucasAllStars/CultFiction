using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int requiredAmountToWin;
    public int amountOfClientsToDeny;
    public GameObject endScreen;
    public Text endText;
    public Text moneyIndicator;
    public Text moneyMultiplierIndicator;
    public Text deniedCallersIndicator;
    public Text highLevelText;

    private int _money;
    private float _moneyMultiplier;
    private int _clientsDenied;
    private int _highLevel;
    private bool _gameRunning;

    public bool GameRunning
    {
        get
        {
            return _gameRunning;
        }
    }

    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = Mathf.CeilToInt(value * _moneyMultiplier);
            moneyIndicator.text = "Your Money: " + _money;
            if (_money >= requiredAmountToWin)
                EndGame(true, "You collected enough money, nice going!");
        }
    }

    public float MoneyMultiplier
    {
        get
        {
            return _moneyMultiplier;
        }
        set
        {
            _moneyMultiplier = 1 + (0.5f * value);
            moneyMultiplierIndicator.text = "Multiplier: X" + (_moneyMultiplier - 1);
        }
    }

    public int ClientsDenied
    {
        get
        {
            return _clientsDenied;
        }
        set
        {
            _clientsDenied = value;
            deniedCallersIndicator.text = "Callers denied: " + _clientsDenied;
            if (_clientsDenied >= amountOfClientsToDeny)
                EndGame(false, "You denied too many legit buyers their dope, ain't nobody buying from you anymore...");
        }
    }

    public int HighLevel
    {
        get
        {
            return _highLevel;
        }
        set
        {
            _highLevel = value;
            highLevelText.text = "Highness level: " + _highLevel;
        }
    }

    void Start()
    {
        moneyIndicator.text = "Your Money: " + 0;
        moneyMultiplierIndicator.text = "Multiplier: X" + 0;
        deniedCallersIndicator.text = "Callers denied: " + 0;
        highLevelText.text = "Highness level: " + 0;
        endScreen.SetActive(false);
        _gameRunning = true;
    }

    public void EndGame(bool endstate, string screenMessage)
    {
        Time.timeScale = 0;
        endScreen.SetActive(true);
        _gameRunning = false;
        endText.text = (endstate ? "You've won!\n" : "You've lost\n") + screenMessage;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
