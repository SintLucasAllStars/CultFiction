using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //When this button is clicked the scene will switch to the playable scene.
    public void PlayBtnClicked()
    {
        //Loads the "Trench" scene.
        SceneManager.LoadScene(1);
    }

    //When this button is clicked the scene will close the game.
    public void ExitButnClicked()
    {
        Application.Quit();
    }
}
