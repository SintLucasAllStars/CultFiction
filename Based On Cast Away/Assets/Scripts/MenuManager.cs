using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //switch to game scene
    public void StartGame()
    {
        SceneManager.LoadScene("ingame");
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
