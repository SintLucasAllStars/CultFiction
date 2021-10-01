using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject gameCanvas;
    public GameObject infoBox;
    public GameObject inGameMenuCanvas;
    private Animation camAnim;
    private CameraMovement camMove;

    private void Start()
    {
        camAnim = mainCamera.GetComponent<Animation>();
        camMove = mainCamera.GetComponent<CameraMovement>();
    }

    public void PlayButtonAct()
    {
        camMove.blockInput = false;
        camAnim.Play("FlyInCamera");
        gameCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void HelpButtonAct()
    {
        if (!infoBox.activeSelf)
        {
            infoBox.SetActive(true);
        }
        else if (infoBox.activeSelf)
        {
            infoBox.SetActive(false);
        }

    }

    public void ExitButtonAct()
    {
        Application.Quit();
    }

    public void MenuButtonAct()
    {
        SceneManager.LoadScene("Level");
    }

    public void RetunButtonAct()
    {
        inGameMenuCanvas.SetActive(false);
    }
}
