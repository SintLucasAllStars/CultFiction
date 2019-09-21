using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Singleton : MonoBehaviour
{
    public static Singleton brain;
    public int Score;
    public Text scoreText;
    void Awake()
    {
        if(Singleton.brain == null)
        {
            Singleton.brain = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Score = 0;
        scoreText.text = "Score" + Score;

    }

    public void addScore(int amt)
    {
        Score += amt;
        scoreText.text = "Score" + Score;
    }
}
