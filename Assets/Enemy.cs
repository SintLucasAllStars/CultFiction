using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    int health = 100;

    public void Damage(int damge)
    {
        health -= damge;
        if(health < 0)
            Destroy(gameObject);
    }
}
