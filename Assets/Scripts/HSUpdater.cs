using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSUpdater : MonoBehaviour      // Updates highscore tabs when menu is loaded
{

    public Text[] hsText;

    void Start()
    {

        SaveLoad.Load();

        for (int i = 0; i < hsText.Length; i++)
        {
            hsText[i].text = GameManager.instance.scores[i].ToString();
        }
    }

    void OnEnable() {
        for (int i = 0; i < hsText.Length; i++)
        {
            hsText[i].text = GameManager.instance.scores[i].ToString();
        }
    }

}