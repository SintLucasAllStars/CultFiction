using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenUiIteractions : MonoBehaviour
{
    public void StartGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level 1");
        StartCoroutine(loadScene(operation));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator loadScene(AsyncOperation operation)
    {
        print(operation.progress);
        yield return new WaitWhile(() => operation.isDone);
        SceneManager.UnloadSceneAsync("Mainscreen");
    }
}
