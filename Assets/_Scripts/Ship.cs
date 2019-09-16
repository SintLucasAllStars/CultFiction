using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int health;
    public float shipSpeed;
    public float dashSpeed;

    [Header("Damage FX")]
    public GameObject[] damageStates;

    public void TakeDamage(int damage)
    {
        health -= damage;
        CheckPlayerStates(health);
    }

    void CheckPlayerStates(int health)
    {
        if (health > (health / 2))
        {
            damageStates[0].SetActive(true);
        }
        if (health < (health/2))
        {
            damageStates[1].SetActive(true);
        }
        if (health < 1)
        {
            damageStates[2].SetActive(true);
        }
    }

}
