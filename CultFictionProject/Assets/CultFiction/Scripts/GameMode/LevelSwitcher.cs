using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{

    public void OpenLevel()
    {
        SceneManager.LoadScene("SC_TestScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
