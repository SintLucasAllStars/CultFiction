using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

    private void Start()
    {
        Time.timeScale = 1;
        _anim.SetTrigger("CameraMov");
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        ScoreManagement.Instance.ToggleUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
