using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Var for UI and camera.
    public GameObject gameUI, deathUI;
    private Camera cam;

    //Var for ducking methode.
    public bool _isDucked;

    //Var for timer.
    public float timeRemaining;
    public Text timerText;

    //Var for godmode.
    private bool godmode = false;

    private void Start()
    {
        //Makes sure the UI is on.
        gameUI.SetActive(true);
        //Sets the deathScreen off.
        deathUI.SetActive(false);
    }

    private void Update()
    {
        //This will count down the time and update the UI component with it.
        if (timeRemaining > 0)
        {
            if (!godmode)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = "Time: " + Mathf.Round(timeRemaining);
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && Input.GetKeyDown(KeyCode.O))
        {
            //Activate Godmode.
            godmode = true;
            timerText.text = "GODMODE";
            timerText.color = Color.red;
            Debug.LogWarning("GODMODE ACTIVATED");
        }
    }

    public void PlayerDeath()
    {
        if (!godmode)
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
            var player = GameObject.Find("Player").GetComponent<PlayerMovement>();
            player.enabled = false;
        }
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
