using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int requiredAmountToWin;
    public int amountOfClientsToDeny;

    private int _money;
    private int _clientsDenied;

    private GameObject endScreen;
    private Text endText;

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
        endScreen = GameObject.Find("EndGameBox");
        endText = GameObject.Find("EndGameText").GetComponent<Text>();
        endScreen.SetActive(false);
    }

    public void EndGame(bool endstate, string screenMessage)
    {
        endScreen.SetActive(true);
        endText.text = (endstate ? "You've won!\n" : "You've lost\n") + screenMessage;
    }
}
