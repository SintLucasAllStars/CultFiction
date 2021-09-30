using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Var for UI and camera.
    public GameObject gameUI, deathUI, winUI;
    private Camera cam, winCam;

    //Var for ducking methode.
    public bool _isDucked;

    //Var for timer.
    public float timeRemaining;
    public Text timerText;

    private GameObject player;

    //Var for godmode.
    private bool godmode = false;

    private void Start()
    {
        //Makes sure the UI is on.
        gameUI.SetActive(true);
        //Sets the deathScreen off.
        deathUI.SetActive(false);
        //Sets the winScreen off.
        winUI.SetActive(false);

        //Gets and disables the win cam.
        winCam = GameObject.Find("WinCam").GetComponent<Camera>();
        winCam.enabled = false;
        
        //Gets the player.
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && Input.GetKeyDown(KeyCode.O))
        {
            //Activate Godmode.
            godmode = true;
            timerText.text = "GODMODE";
            timerText.color = Color.red;
            Debug.LogWarning("GODMODE ACTIVATED");
        }

        //This will count down the time and update the UI component with it.
        if (timeRemaining > 0)
        {
            if (!godmode)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = "Time: " + Mathf.Round(timeRemaining);
            }
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
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void ActivateSniper()
    {
        var _sniper = GameObject.Find("Sniper").GetComponent<SniperHandeler>();
        _sniper.isDucked = _isDucked;
    }

    public IEnumerator Win()
    {
        //Switch camara.
        yield return new WaitForSecondsRealtime(3);
        GameObject.Find("WinCam").GetComponent<Camera>();
        winCam.enabled = true;

        //Disables the player camera.
        cam = Camera.main;
        cam.GetComponent<Camera>().enabled = false;

        //Turns of the audio of the player camara.
        AudioListener camAudio = GameObject.Find("MainCamera").GetComponent<AudioListener>(); ;
        camAudio.enabled = false;

        //Disables the whole player so he can't break the game while in the win menu.
        GameObject player = GameObject.Find("Player");
        player.SetActive(false);

        //Disable grenade spawner.
        GameObject grenadeSpawner = GameObject.Find("GrenadeSpawners");
        grenadeSpawner.SetActive(false);

        //Enables the UI.
        gameUI.SetActive(false);
        winUI.SetActive(true);

        //Plays the song.
        AudioSource american_song = GameObject.Find("WinAudio").GetComponent<AudioSource>();
        american_song.Play();

        //Enables the aplha to be changed.
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Image image = GameObject.Find("Background").GetComponent<Image>();

        float targetAlpha = 1.0f;
        Color curColor = image.color;
        yield return new WaitForSecondsRealtime(15);
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            Debug.Log(image.material.color.a);
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, 3f * Time.deltaTime);
            image.color = curColor;
            yield return null;
        }

        //If the fade is done, load another scene.
        SceneManager.LoadScene(0);
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
