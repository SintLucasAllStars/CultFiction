using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : Singleton<KitchenManager>
{
    [SerializeField]
    private List<Food> _foods = new List<Food>();
    public List<Food> Foods => _foods;

    private List<Machine> _machines = new List<Machine>();

    public void FindMachines() => _machines = new List<Machine>(FindObjectsOfType<Machine>());

    public bool CanOrder(FoodName _foodName)
    {
        bool canOrder = false;
        for (int i = 0; i < _machines.Count; i++)
        {
            if (_machines[i].FoodType == _foodName)
                canOrder = true;
        }
        return canOrder;
    }

    public void AddFood(Food food) => _foods.Add(food);
}
