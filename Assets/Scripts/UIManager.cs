using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image health;
    public Image armor;
    public TMP_Text ammoCapacity;

    public Sprite[] healths;

    private void OnEnable()
    {
        Player.OnChange += UpdateUI;
    }

    private void OnDisable()
    {
        Player.OnChange -= UpdateUI;
    }

    private void UpdateUI(int _health, int _ammo, int _ammoInClip)
    {
        if(_health < 5)
        {
            health.sprite = healths[_health];
        }
        else
        {
            armor.sprite = healths[_health];
        }

        string capacity = _ammoInClip + " / " + _ammo;
        ammoCapacity.text = capacity;
    }
}
