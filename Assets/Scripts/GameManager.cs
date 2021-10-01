using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public float FinalTime = 100000;
    private float highScore = 100000;
    public string highScoreText;
    public string curTime;

    private float hours = 0;
    private float minutes = 0;
    private float seconds = 0;
    private float curSeconds;

    private bool isTiming = false;
    private float startTime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (isTiming)
        {
            GetTimeTable(Time.time - startTime);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SetTimer(bool goTime = true)
    {
        if(goTime)
        {
            curTime = "0:0.0";
            startTime = Time.time;
            isTiming = goTime;
        }
        else /*(!goTime)*/
        {
            FinalTime = Time.time - startTime;
            if (FinalTime < highScore)
            {
                highScore = FinalTime;
                highScoreText = curTime;
            }
            ResetValues();
        }
    }

    private void ResetValues()
    {
        hours = 0;
        minutes = 0;
        seconds = 0;
        curSeconds = 0;
        FinalTime = 0;
        startTime = 0;
        isTiming = false;
    }

    public List<float> GetTimeTable(float parseTime)
    {
        List<float> times = new List<float>();

        translateToTime(parseTime);

        times.Add(hours);
        times.Add(minutes);
        times.Add(seconds);
        times.Add(parseTime - seconds + minutes * 60 + hours * 3600);
        curTime = times[1].ToString() + ":" + times[2].ToString() + "." + Mathf.RoundToInt(times[3]*100).ToString();
        //Debug.Log(times[0] + " : " + times[1] + " : " + times[2] + " : " + times[3]);
        return times;
    }

    private string TimeConvertToString(float time)
    {
        translateToTime(time);
        return  minutes.ToString() + ":" + seconds.ToString()+ "." + (time - seconds + minutes * 60 + hours * 3600).ToString();
    }

    private void translateToTime(float useVal)
    {
        if (useVal - curSeconds > 1)
        {
            seconds++;
            curSeconds++;
        }
        if (seconds >= 60)
        {
            minutes++;
            seconds -= 60;
        }

        if (minutes >= 60)
        {
            hours++;
            minutes -= 60;
        }
    }
}
