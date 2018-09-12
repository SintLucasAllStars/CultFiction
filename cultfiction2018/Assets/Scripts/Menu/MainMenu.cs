using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    public void GoToRegisterScene()
    {
        SceneManager.LoadScene("RegisterMenu");
    }

    public void GoToLoginScene()
    {
        SceneManager.LoadScene("LogInMenu");
    }
    
}
