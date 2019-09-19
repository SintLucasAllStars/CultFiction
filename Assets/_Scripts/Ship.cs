using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int health;
    private int maxHealth;
    public float shipSpeed;
    public float dashSpeed;

    [Header("Damage FX")]
    public GameObject[] damageStates;

    private void Awake()
    {
        maxHealth = health;
    }
    
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        CheckDamageStates(health);
    }

    void CheckDamageStates(int health)
    {
        if (health > (maxHealth / 2))
        {
            damageStates[0].SetActive(true);
        }
        else if (health < (maxHealth / 2))
        {
            damageStates[1].SetActive(true);
        }
        if (health < 1)
        {
            damageStates[2].SetActive(true);
        }
    }

}
