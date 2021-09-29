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
    // Start is called before the first frame update
    void Start()
    {
        playervan.GetComponent<CarControl>().driveable = false;
        counterBackground.SetActive(false);
        StartCoroutine("StartRace");
    }

    // Update is called once per frame
    void Update()
    {
        
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
        counterBackground.SetActive(false);
        playervan.GetComponent<CarControl>().driveable = true;
        yield return null;
    }
}
