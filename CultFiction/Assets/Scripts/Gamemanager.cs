using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public int points;
    public int timesShot;
    public List<Image> healthImages;
    [SerializeField]
    Text scoreTxt;
    [SerializeField]
    Text timeShotTxt;
    [SerializeField]
    Text HighscoreTxt;
    

    private void Awake()
    {
        instance = this;
        HighscoreTxt.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
    }

    public void UpdatePoints(int addedPoints)
    {
        points += addedPoints;
        scoreTxt.text = "Total kills: " + points.ToString();
    }

    public void UpdateTimesShot()
    {
        timesShot++;
        timeShotTxt.text = "Total balls shot: " + timesShot.ToString();
    }

    public void UpdateHealth()
    {
        int curImg = healthImages.Count;
        healthImages[curImg -1].enabled = false;
        healthImages.RemoveAt(curImg -1);
        if(healthImages.Count == 0)
        {
            SaveHighscore();
            SceneManager.LoadScene(3);
            Debug.Log("You died, you failed to defend your planet. The dark lord has taken over");
        }
    }

    public void SaveHighscore()
    {
        int prefHigh = PlayerPrefs.GetInt("Highscore");
        if (points > prefHigh)
            PlayerPrefs.SetInt("Highscore", points);
        else
            Debug.Log("Highscore not reached");
    }
}
