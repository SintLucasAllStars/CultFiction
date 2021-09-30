using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public TextMeshProUGUI timeCounter;
    public TextMeshProUGUI FinaltimeCounter;
    public GameObject ingameTimer;
    public GameObject finalTimer;
    public GameObject playerVan;

    public GameObject firworks;


    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    private void Awake()
    {
        instance = this;
    }
    

    private void Start()
    {
        FinaltimeCounter.text = "Time: 00:00.00";
        timeCounter.text = "Time: 00:00.00";
        timerGoing = false;
        finalTimer.SetActive(false);
        ingameTimer.SetActive(true);
        firworks.SetActive(false);
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        finalTimer.SetActive(false);
        ingameTimer.SetActive(true);
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
        finalTimer.SetActive(true);
        ingameTimer.SetActive(false);
        StartCoroutine("stopDriveable");
        firworks.SetActive(true);
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
            FinaltimeCounter.text = timePlayingStr;

            yield return null;
        }
    }

    public IEnumerator stopDriveable()
    {
        yield return new WaitForSecondsRealtime(1);
        playerVan.GetComponent<CarControl>().driveable = false;
        yield return null;
    }
}
