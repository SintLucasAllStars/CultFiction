using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    public Text timeText;

    private void Update()
    {
        timeText.text = "Time: " + GameManager.instance.curTime;
    }
}
