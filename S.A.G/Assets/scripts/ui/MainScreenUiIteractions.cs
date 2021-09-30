using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenUiIteractions : MonoBehaviour
{
    public Dropdown difficultySelecter;

    public void StartGame()
    {
        GameManager.manager.changeDifficulty(difficultySelecter.value);
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level 1");
        StartCoroutine(loadScene(operation));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator loadScene(AsyncOperation operation)
    {
        yield return new WaitWhile(() => operation.isDone);
        SceneManager.UnloadSceneAsync("Mainscreen");
    }
}
