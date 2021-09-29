using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceManager : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public GameObject counterBackground;
    public GameObject playervan;
    public GameObject timecontroller;
    public GameObject finish;
    public GameObject checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        playervan.GetComponent<CarControl>().driveable = false;
        counterBackground.SetActive(false);
        checkpoint.SetActive(true);
        StartCoroutine("StartRace");
        finish.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playervan.GetComponent<CarControl>().finished == true)
        {
            timecontroller.GetComponent<TimerController>().EndTimer();
        }
        if (playervan.GetComponent<CarControl>().checkpointed == true)
        {
            finish.SetActive(true);
            checkpoint.SetActive(false);
        }
    }

    //start sequence for start race
    IEnumerator StartRace()
    {
        counterBackground.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        counterText.text = "3";
        yield return new WaitForSecondsRealtime(1);
        counterText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        counterText.text = "1";
        yield return new WaitForSecondsRealtime(1); 
        counterText.text = "GO!";
        yield return new WaitForSecondsRealtime(1);
        timecontroller.GetComponent<TimerController>().BeginTimer();
        counterBackground.SetActive(false);
        playervan.GetComponent<CarControl>().driveable = true;
        yield return null;
    }
}
