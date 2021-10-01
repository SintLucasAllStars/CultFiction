using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUiManager : MonoBehaviour
{

    public Text scoreElement;
    private int score = 0;

    // Update is called once per frame
    void Update()
    {
        if(score != GameManager.manager.score)
        {
            score = GameManager.manager.score;
            scoreElement.text = (score * 100).ToString();
        }
    }
}
