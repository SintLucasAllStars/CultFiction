using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackSystem : MonoBehaviour
{
    public Text _feedbackText;
    private bool isPlaying;
    public Text warning;

    private void Start()
    {
        _feedbackText.enabled = false;
    }

    public void ShowFeedback(string feedbackMessage)
    {
        _feedbackText.enabled = true;
        _feedbackText.text = feedbackMessage;
    }

    public void HideFeedback()
    {
        _feedbackText.enabled = false;
        _feedbackText.text = "";
    }

    public void PlayWarning(string msg, int time)
    {
        if (isPlaying)
        {
            return;
        }

        StartCoroutine(ShowMessage(msg, time));
    }

    private IEnumerator ShowMessage(string msg, int time)
    {
        isPlaying = true;
        warning.text = msg;
        yield return new WaitForSeconds(time);
        warning.text = "";
        isPlaying = false;
    }
}