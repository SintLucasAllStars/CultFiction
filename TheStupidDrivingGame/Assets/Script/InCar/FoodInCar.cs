using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInCar : InCar
{
    public int foodPoints;

    protected override void OnEat()
    {
        FindObjectOfType<Player>().UpdateFood(foodPoints);
        FindObjectOfType<Driving>().RemoveRigidbody(GetComponent<Rigidbody>());
        Destroy(this.gameObject);
    }
}
