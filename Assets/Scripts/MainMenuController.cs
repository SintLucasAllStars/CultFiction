using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameObject noteCanvas;

    private void Start()
    {
        noteCanvas =  GameObject.Find("NoteCanvas");
        noteCanvas.SetActive(false);

    }

    //When this button is clicked the scene will switch to the playable scene.
    public void PlayBtnClicked()
    {
        //Loads the "Trench" scene.
        SceneManager.LoadScene(1);
    }

    //When this button is clicked the scene will close the game.
    public void ExitButnClicked()
    {
        //Quits the game.
        Application.Quit();
    }

    public void HowToPlayClicked()
    {
        noteCanvas.SetActive(true);
    }

    public void CloseClicked()
    {
        noteCanvas.SetActive(false);
    }
}