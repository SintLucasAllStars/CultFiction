using UnityEngine;
using TMPro;

[System.Serializable]
public class BankAccount : Singleton<BankAccount>
{
    [SerializeField]
    private int _money = 100;
    public int Money => _money;

    [SerializeField]
    private TextMeshProUGUI _moneyText = null;

    private void Start() => UpdateText();

    public void AddMoney(int i)
    {
        if (i < 0)
            return;

        _money += i;
        UpdateText();
    }

    public void RemoveMoney(int i)
    {
        if (i < 0)
            return;

        _money -= i;
        UpdateText();
    }

    public bool HasEnoughMoney(int price)
    {
        if (price > _money)
            return false;

        return true;
    }

    private void UpdateText() => _moneyText.text = _money.ToString();
}
