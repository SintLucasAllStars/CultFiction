using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public TMP_Text victoryText;
    public TMP_Text instructionText;

    private bool _victory;

    private void OnTriggerEnter(Collider other)
    {
        victoryText.enabled = true;
        instructionText.enabled = true;
        _victory = true;
    }

    private void Update()
    {
        if (!_victory) return;

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Player.Instance.OnDeath();
            TiggerManager.ResetTriggers();
            victoryText.enabled = false;
            instructionText.enabled = false;
        }
    }
}
