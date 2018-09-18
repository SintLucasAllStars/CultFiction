using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Item;


[CreateAssetMenu(fileName = "Food", menuName = "Inventory/Food", order = 4)]
public class Food : Item
{
    public int HealingValue = 20;


    public override void Use(int index)
    {
        Debug.Log("Eating");
        PlayerController.instance.health += HealingValue;
        PlayerController.instance.DestroyItem(index);
       //GameObject.Find("Player").GetComponent<PlayerMotor>().
    }
}
