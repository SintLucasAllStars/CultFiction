using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        BoxScript[] bs = FindObjectsOfType<BoxScript>();
        foreach (BoxScript b in bs)
        {
            Destroy(b);
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}