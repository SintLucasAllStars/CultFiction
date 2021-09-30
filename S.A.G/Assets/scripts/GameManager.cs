using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public int dificulty;
    public int score = 0;
    public int planeAmountInLevel = 0;
    public int killCount = 0;

    private void Awake()
    {
        if (manager is null)
        {
            manager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (score < 0)
        {
            score = 0;
        }
    }

    public void scoreIncrease()
    {
        score++;
    }

    public void scoreDecrease()
    {
        score--;
    }

    public void changeDifficulty(int value)
    {
        dificulty = value;
    }

    public void UpdatePlaneAmount(int value)
    {
        planeAmountInLevel = value;
    }

    public void UpdatePlaneKillCount()
    {
        killCount++;
        if(killCount >= planeAmountInLevel)
        {
            SceneManager.LoadScene("MainScreen");
        }
    }
}
