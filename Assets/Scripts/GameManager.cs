using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameUI, deathUI;
    private Camera cam;

    public bool _isDucked;

    private void Start()
    {
        //Makes sure the UI is on.
        gameUI.SetActive(true);
        //Sets the deathScreen off.
        deathUI.SetActive(false);
    }

    public void PlayerDeath()
    {
        //Opens the right UI.
        deathUI.SetActive(true);
        gameUI.SetActive(false);

        //Gets the camera with the 'MainCamera' tag.
        cam = Camera.main;
        //Removes and locks the cursor in the middle of the screen.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //Disable camera movement.
        cam.GetComponent<CameraHandler>().enabled = false;

        //Disable player movement.
        var player = GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
    }

    public void ActivateSniper()
    {
        var _sniper = GameObject.Find("Sniper").GetComponent<SniperHandeler>();
        _sniper.isDucked = _isDucked;
    }

    public void RestartBtnClicked()
    {
        //Restart the game.
        SceneManager.LoadScene(1);
    }

    public void ExitBtnClicked()
    {
        //Exit to the Main menu.
        SceneManager.LoadScene(0);
    }
}
