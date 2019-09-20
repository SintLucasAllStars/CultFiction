using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Interactable
{
    private int _reward;

    public void Init(int reward) => _reward = reward;

    protected override bool Interact()
    {
        if (!base.Interact())
            return false;
        BankAccount.Instance.AddMoney(_reward);
        SoundEffectManager.Instance.PlaySound(SoundEffectName.Money);
        Destroy(this.gameObject);
        return true;
    }
}
