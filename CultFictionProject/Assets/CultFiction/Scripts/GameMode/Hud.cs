using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    [SerializeField]
    private TMP_Text enemyCounter;
    [SerializeField]
    private TMP_Text VictoryTxt;
    [SerializeField]
    private TMP_Text LostTxt;
    [SerializeField]
    private TMP_Text HealthTxt;


    // Start is called before the first frame update
    void Start()
    {
        VictoryTxt.enabled = false;
        LostTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnemyCounter(int currentEnemies, int MaxEnemies)
    {
        enemyCounter.text = currentEnemies + "/" + MaxEnemies + " Enemies Left";
    }

    public void EnableVictory()
    {
        VictoryTxt.enabled = true;
    }
    public void EnableFail()
    {
        LostTxt.enabled = true;
    }

    public void UpdateHealthTxt(int health)
    {
        HealthTxt.text = "Health: " + health.ToString();
    }
}
