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

    public bool gameRunning { get; private set; }
    private int money;
    private float moneyMultiplier;
    private int clientsDenied;
    private int highLevel;

    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = Mathf.CeilToInt(value * moneyMultiplier);
            moneyIndicator.text = "Your Money: " + money;
            if (money >= requiredAmountToWin)
                EndGame(true, "You collected enough money to pay rent, nice going!");
        }
    }

    public float MoneyMultiplier
    {
        get
        {
            return moneyMultiplier;
        }
        set
        {
            moneyMultiplier = 1 + (0.5f * value);
            moneyMultiplierIndicator.text = "Multiplier: X" + (moneyMultiplier - 1);
        }
    }

    public int ClientsDenied
    {
        get
        {
            return clientsDenied;
        }
        set
        {
            clientsDenied = value;
            deniedCallersIndicator.text = "Callers denied: " + clientsDenied;
            if (clientsDenied >= amountOfClientsToDeny)
                EndGame(false, "You denied too many legit buyers their dope, ain't nobody buying from you anymore...");
        }
    }

    public int HighLevel
    {
        get
        {
            return highLevel;
        }
        set
        {
            highLevel = value;
            highLevelText.text = "Highness level: " + highLevel;
        }
    }

    void Start()
    {
        moneyIndicator.text             = "Your Money: " + 0;
        moneyMultiplierIndicator.text   = "Multiplier: X" + 0;
        deniedCallersIndicator.text     = "Callers denied: " + 0;
        highLevelText.text              = "Highness level: " + 0;
        moneyMultiplier                 = 1;
        gameRunning                     = true;
        endScreen.SetActive(false);
    }

    public void EndGame(bool endstate, string screenMessage)
    {
        endScreen.SetActive(true);
        Time.timeScale  = 0;
        gameRunning     = false;
        endText.text    = (endstate ? "You've won!\n" : "You've lost\n") + screenMessage;
    }
}
