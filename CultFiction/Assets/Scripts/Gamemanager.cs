using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public int points;
    TextMeshProUGUI scoreTxt;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePoints(int addedPoints)
    {
        points += addedPoints;
        scoreTxt.text = points.ToString();
    }
}
