using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour, IInteractable
{
    [SerializeField] int requiredPoints;

    public void Interact(PlayerController controller)
    {
        Debug.Log(Gamemanager.instance.GetScore());
        if(Gamemanager.instance.GetScore() >= requiredPoints)
        {
            Debug.Log("realod baby");
            Gamemanager.instance.AddToScore(-requiredPoints);
            controller.currentWeapon.FillAmmo();
            controller.UpdateAmmo();
        }
    }
}
