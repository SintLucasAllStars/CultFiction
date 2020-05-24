using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            ani = GetComponent<Animator>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        ani.Play("Level_FadeIn");
        GameManager.instance.TogglePlayer(true);
    }

    public void LevelChange(string sceneName)
    {
        GameManager gm = GameManager.instance;
        if (gm != null)
        {
            GameManager.instance.TogglePlayer(false);
        }
        Debug.Log("Loading...");
        ani.Play("Level_FadeOut");
        StartCoroutine(FadeOutChange(sceneName));
    }

    IEnumerator FadeOutChange(string sceneName)
    {
        AnimatorStateInfo currInfo = ani.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currInfo.normalizedTime);
        if (sceneName != "Quit")
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Application.Quit();
        }
    }
}
