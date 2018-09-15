using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour, IInteractable
{
    [SerializeField] int requiredPoints;

    public void Interact(PlayerController controller)
    {
        if(Gamemanager.instance.GetScore() > requiredPoints)
        {
            Gamemanager.instance.AddToScore(requiredPoints);
            controller.currentWeapon.FillAmmo();
        }
    }
}
