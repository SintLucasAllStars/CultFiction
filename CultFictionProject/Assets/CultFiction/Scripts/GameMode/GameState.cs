using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public Hud hud;
    public int CurrentEnemies = 0;

    [SerializeField]
    private PlayerController pc;
    private int maxEnemies = 0;
    private bool gameHasEnded;
 

    // Start is called before the first frame update
    void Start()
    {
        maxEnemies = GameObject.FindGameObjectsWithTag("AI").Length;
        CurrentEnemies = maxEnemies;
        
    }

    // Update is called once per frame
    void Update()
    {
        hud.SetEnemyCounter(CurrentEnemies, maxEnemies);
        hud.UpdateHealthTxt(pc.Health);

        if(CurrentEnemies <= 0 && !gameHasEnded)
        {
            //EndGame player wins!!!
            hud.EnableVictory();
            StartCoroutine("EndDelay");
            gameHasEnded = true;
        }
        if(pc.Health <= 0 && !gameHasEnded)
        {
            hud.EnableFail();
            StartCoroutine("EndDelay");
            gameHasEnded = true;
          
        }
    }


    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}
