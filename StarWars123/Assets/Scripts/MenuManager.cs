using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject InGameUi;
    public GameObject GameOverMenu;
    public GameObject StartGameMenu;

    public static MenuManager MenuManagerInstance;

    private void Start() {
        MenuManagerInstance = this;
    }

    public void ReloadGame() {
        SceneManager.LoadScene(1);
    }

    public void EnableInGameUi() {
        InGameUi.SetActive(true);
    }
    public void DisableIngameUi() {
        InGameUi.SetActive(false);
    }

    public void EnableStartGameUi() {
        StartGameMenu.SetActive(true);
    }
    public void DisableStartGameUi() {
        StartGameMenu.SetActive(false);
    }

    public void EnableGameOverUi() {
        GameOverMenu.SetActive(true);
    }
    public void DisableGameOverUi() {
        GameOverMenu.SetActive(false);
    }
}
