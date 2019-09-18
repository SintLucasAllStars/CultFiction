using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pressAnyButtonToStart;
    public GameObject playerCamera;
    public GameObject uiCamera;
    private bool gameStarted;

    public Animator anim;

    private void Update()
    {
        if (Input.anyKey && gameStarted == false)
        {
            StartGame();
            gameStarted = true;
        }
    }

    public void GivePlayerControl()
    {
        GameObject.FindObjectOfType<PlayerFlyController>().playerHasControl = true;
        uiCamera.SetActive(false);
        playerCamera.SetActive(true);
    }

    public void StartGame()
    {
        pressAnyButtonToStart.SetActive(false);
        anim.SetBool("Start", true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
