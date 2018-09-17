using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public bool fadeOnStartUp;
    public bool fadeOnSceneSwitch;
    public Image blackFade;

    public void SwitchScene(int index)
    {
        Time.timeScale = 1f;
        if (fadeOnSceneSwitch)
        {
            StartCoroutine(SceneFadeCoroutine(false, index));
        }
        else
        {
            SceneManager.LoadScene(index);
        }
    }

    void Start()
    {
        if (fadeOnStartUp)
        {
            StartCoroutine(SceneFadeCoroutine(true, 0));
        }
        else
        {
            blackFade.gameObject.SetActive(false);
            blackFade.enabled = false;
        }
    }

    private IEnumerator SceneFadeCoroutine(bool fadeIn, int index)
    {
        blackFade.gameObject.SetActive(true);
        blackFade.enabled = true;

        Color startColor    = blackFade.color;
        startColor.a        = fadeIn ? 1f : 0f;
        blackFade.color     = startColor;

        for (int i = 0; i < 10; i++)
        {
            Color newColor  = blackFade.color;
            newColor.a      += fadeIn ? -0.1f : 0.1f;
            Debug.Log("test");
            blackFade.color = newColor;
            yield return null;
        }

        if (!fadeIn)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            blackFade.gameObject.SetActive(false);
            blackFade.enabled = false;
        }
    }
}
