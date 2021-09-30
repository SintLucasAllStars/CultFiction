using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Var for UI and camera.
    public GameObject gameUI, deathUI, endUI;
    private Camera cam, winCam;

    public Text endText;

    //Var for ducking methode.
    public bool _isDucked;

    //Var for timer.
    public float timeRemaining;
    public Text timerText;

    private GameObject player, endCam;

    //Var for godmode.
    private bool godmode = false;

    //Array for the end explosion
    GameObject[] bomb;
    public GameObject ExplosionFX;
    private Coroutine endEx;
    private float fadeTime;

    private void Start()
    {
        //Makes sure the UI is on.
        gameUI.SetActive(true);
        //Sets the deathScreen off.
        deathUI.SetActive(false);
        //Sets the endScreen off.
        endUI.SetActive(false);

        //Gets and disables the win cam.
        winCam = GameObject.Find("WinCam").GetComponent<Camera>();
        winCam.enabled = false;
        
        //Gets the player.
        player = GameObject.Find("Player");

        cam = Camera.main;

        endCam = GameObject.Find("EndCam");
        endCam.SetActive(false);
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
        if (timeRemaining <= 0 && endEx is null)
        {
             endEx = StartCoroutine(End());
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

    private IEnumerator End()
    {
        //Disable the gameUI.
        gameUI.SetActive(false);

        //Enable EndUI.
        endText.text = "WE LOST THE WAR";
        endUI.SetActive(true);

        //Switch camera.
        endCam.SetActive(true);

        cam.enabled = false;

        //Disable player movement.
        player.GetComponent<PlayerMovement>().enabled = false;

        //Play rocket sound.
        AudioSource _v1Sound = GameObject.Find("V1Rocket").GetComponent<AudioSource>();
        _v1Sound.Play();

        //Gets all the bomb locations.
        bomb = GameObject.FindGameObjectsWithTag("BombEnd");

        yield return new WaitForSecondsRealtime(4);

        //Play explosion sound.
        AudioSource _v1Exp = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
        _v1Exp.Play();

        //Spawn explosions.
        foreach (var item in bomb)
        {
            print(item.GetComponent<Transform>().position);
            Instantiate(ExplosionFX, item.transform.position, Quaternion.Euler (-90,0,0));
        }

        fadeTime = 3;

        //FadeIn and switch screen.
        StartCoroutine(FadeIn(fadeTime));
    }

    public IEnumerator Win()
    {
        //Switch camara.
        yield return new WaitForSecondsRealtime(3);
        GameObject.Find("WinCam").GetComponent<Camera>();
        winCam.enabled = true;

        //Disables the player camera.
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

        endText.text = "WE WON THE WAR";
        endUI.SetActive(true);

        //Plays the song.
        AudioSource american_song = GameObject.Find("WinAudio").GetComponent<AudioSource>();
        american_song.Play();

        fadeTime = 15;

        //Enables the aplha to be changed.
        StartCoroutine(FadeIn(fadeTime));
    }

    IEnumerator FadeIn(float time)
    {
        Image image = GameObject.Find("Background").GetComponent<Image>();

        float targetAlpha = 1.0f;
        Color curColor = image.color;
        yield return new WaitForSecondsRealtime(time);
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            Debug.Log(image.material.color.a);
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, 4f * Time.deltaTime);
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
