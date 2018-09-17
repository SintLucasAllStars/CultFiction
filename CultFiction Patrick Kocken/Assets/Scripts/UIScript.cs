using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIScript : MonoBehaviour {
    
    [SerializeField] Animator _titleText;
    [SerializeField] Animator _startButton;
    [SerializeField] Image _fadeImage;


    public void StartFade(bool endGame)
    {
        StartCoroutine(Fade(endGame));
    }

    private IEnumerator Fade(bool endGame){

        float start = Time.fixedTime;
        float elapsed = 0;
        float duration = 3;

        while (elapsed < duration)
        {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
            _fadeImage.color = Color.Lerp(_fadeImage.color, new Color(0, 0, 0,Convert.ToInt32(endGame) ), normalisedTime);
            yield return null;
        }

        if(endGame)
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

	public void IntroScroll(){

        _titleText.enabled = true;

        _startButton.SetLayerWeight(0, 0);
        _startButton.SetLayerWeight(1, 1);
        _startButton.SetBool("Fading", true);
        _startButton.GetComponent<Button>().interactable = false;
        
    }
}
