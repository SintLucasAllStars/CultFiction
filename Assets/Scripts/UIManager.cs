using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _timeCounter;

    [SerializeField]
    private Text _currentSpiderCount;

    [SerializeField]
    private Text _highscoreText;

    [SerializeField]
    private Text _previousHighscore;

    [SerializeField]
    private Text _PreviousTime;

    private void Start()
    {
        ResetUI();
        gameObject.SetActive(false);
    }

    public void UpdateTimer(float currentTime)
    {
        _timeCounter.text = "Time: " + Mathf.RoundToInt(currentTime).ToString();
    }

    public void UpdateSpiderCount(int currentSpiders)
    {
        _currentSpiderCount.text = "Current spiders: " + currentSpiders.ToString();
    }

    public void UpdateHighscoreCount(int newHighscore)
    {
        _highscoreText.text = "Highscore: " + newHighscore.ToString();
    }

    public void UpdateMenuScores(int highscore, float time)
    {
        _previousHighscore.text = "Previous highscore: " + highscore.ToString();
        _PreviousTime.text = "You survived: " + Mathf.RoundToInt(time).ToString() + " seconds";
    }

    public void ResetUI()
    {
        UpdateTimer(0);
        UpdateSpiderCount(0);
        UpdateHighscoreCount(0);
    }
}
