using UnityEngine;
using TMPro;
using System;

public class uiController : MonoBehaviour
{
    #region Singleton
    public static uiController instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    private bool isPaused;


    [SerializeField] private TextMeshProUGUI scoreText;
    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            return;
        }
        Time.timeScale = 0;
        isPaused = !isPaused;
    }
    public void ShowScore(int score)
    {
        scoreText.text = "score: " + score;
    }

    public void WillYouAnswer(bool isAnswering)
    {
        Debug.Log("clicked");
        gameController.instance.isPhoneClicked = isAnswering;
    }

    void UpdateMistakes()
    {

    }
}