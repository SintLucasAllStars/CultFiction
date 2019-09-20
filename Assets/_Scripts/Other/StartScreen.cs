using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public Button start;

    public Button quit;

    public void OnStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
