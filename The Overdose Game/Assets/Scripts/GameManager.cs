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

    private int _money;
    private int _clientsDenied;
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
            _money = value;
            if (_money >= requiredAmountToWin)
                EndGame(true, "You collected enough money, nice going!");
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
            if (_clientsDenied >= amountOfClientsToDeny)
                EndGame(false, "You denied too many legit buyers their dope, ain't nobody buying from you anymore...");
        }
    }

    void Start()
    {
        endScreen.SetActive(false);
        _gameRunning = true;
    }

    public void EndGame(bool endstate, string screenMessage)
    {
        endScreen.SetActive(true);
        _gameRunning = false;
        endText.text = (endstate ? "You've won!\n" : "You've lost\n") + screenMessage;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
