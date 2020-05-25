using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timerValue;
    private bool runTimer = false;
    public Text timerText;
    public UnityEvent TimerEvent;

    // Start is called before the first frame update
    void Start()
    {
        HideUI();
    }
    
    public void StartTimer(float _timerValue)
    {
        timerValue = _timerValue;
        runTimer = true;
    }

    public void StopTimer()
    {
        runTimer = false;
        HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        {
            timerValue -= Time.deltaTime;

            UpdateUI();

            if (timerValue <= 0f)
            {
                StopTimer();

                if (TimerEvent != null)
                {
                    TimerEvent.Invoke();
                }
            }
        }
    }

    private void UpdateUI()
    {
        if (timerValue <= 1f)
        {
            timerText.text = "Start";
        }
        else
        {
            timerText.text = timerValue.ToString("0");
        }
    }

    private void HideUI()
    {
        timerText.text = "";
    }
}
