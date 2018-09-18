using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Inventory.Item;

[CreateAssetMenu(fileName = "Stuff", menuName = "Inventory/Stuff", order = 1)]
public class Stuff : Item
{
    public int throwDistance;


    public override void Use(int index)
    {
        PlayerController.instance.ThrowItem(index, throwDistance);
        Debug.Log("Throw");
    }
}
