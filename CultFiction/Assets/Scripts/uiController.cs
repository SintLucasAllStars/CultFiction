using UnityEngine;
using UnityEngine.UI;

public class uiController : extendedFunctions
{
    #region Singleton
    public static uiController instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
    #endregion

    private bool isPaused;

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            return;
        }
        Time.timeScale = 0;
        isPaused = !isPaused;
    }

    public void SwitchScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void WillYouAnswer(bool isAnswering)
    {
        gameController.instance.isPhoneClicked = isAnswering;
    }

    public void SetButtonColor(Button but)
    {
        var mapped = Map(148, 0, 255, 0, 1);
        var alpha = Map(150, 0, 255, 0, 1);
        but.image.color = new Color(mapped, mapped, mapped, alpha);
    }
}