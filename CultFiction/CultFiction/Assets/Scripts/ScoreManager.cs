using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public List<GameObject> pins = new List<GameObject>();
    public Text score;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Clean()
    {
        Time.timeScale = 1;
        pins.Clear();
        score.text = "";
    }

    public void Tally()
    {
        int total = 0;
        int bonusPoints = 0;
        for (int i = 0; i < pins.Count; i++)
        {
            total++;
            bonusPoints += Mathf.RoundToInt(pins[i].gameObject.transform.localScale.y - 1f);
        }
        score.text = "Score : " + total + "+" + bonusPoints;
    }
}
