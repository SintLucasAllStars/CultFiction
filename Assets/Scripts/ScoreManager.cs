using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public Text scoreText;
    public Text round;
    public GameObject GameOverText;
    float score = 0;
	// Use this for initialization
	void Start () {
        UpdateScore();


    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
    public void UpdateRound(int level)
    {
        round.text = "Round: " + level;
    }
    public void GameOver()
    {
        GameOverText.SetActive(true);
    }

    public void AddScore(float pointToAdd)
    {
        score += pointToAdd;
        UpdateScore();
    }

}
