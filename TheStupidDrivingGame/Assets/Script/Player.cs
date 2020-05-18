using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 3;
    public int currHealth = 3;

    public int maxFood = 100;
    public int currFood = 100;

    public void Eat(int foodPoints)
    {
        if (currFood + foodPoints < maxFood)
            currFood += foodPoints;
        else
            currFood = maxFood;
    }

    public void Hit(int damage)
    {
        if(currHealth - damage > 0)
        {
            currHealth -= damage;
        }
        else
        {
            currHealth = 0;
        }
    }
}
