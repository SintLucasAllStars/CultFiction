using System.Collections;
using System.Collections.Generic;
using LogIn;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Text PlayerDisplayText;
    
    private void Start()
    {
        if (DBmanager.LoggedIn)
        {
            PlayerDisplayText.text = "Player: " + DBmanager.Username;
        }
    }
    
    public void GoToRegisterScene()
    {
        SceneManager.LoadScene("RegisterMenu");
    }

    public void GoToLoginScene()
    {
        SceneManager.LoadScene("LogInMenu");
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    
}
