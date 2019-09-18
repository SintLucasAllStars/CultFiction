using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public int points;
    public int timesShot;
    public List<Image> healthImages;
    [SerializeField]
    TextMeshProUGUI scoreTxt;
    [SerializeField]
    TextMeshProUGUI timeShotTxt;
    

    private void Awake()
    {
        instance = this;
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
            Debug.Log("You died, you failed to defend your planet. The dark lord has taken over");
        }
    }
}
