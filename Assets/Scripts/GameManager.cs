using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int _money = 100;

    [SerializeField]
    private TextMeshProUGUI _moneyText;

    private void Start() => UpdateMoney(0);

    public bool HasEnoughMoney(int price)
    {
        if (price > _money)
            return false;

        return true;
    }

    public void UpdateMoney(int money)
    {
        _money += money;
        _moneyText.text = _money.ToString();
    }
}