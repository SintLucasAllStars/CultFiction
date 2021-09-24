using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 100f, currentHealth;

    public Text healthText;
    private string _text = "Health: ";

    private void Awake()
    {
        //Sets the currentHealth to fixed amount.
        currentHealth = maxHealth;

        healthText.text = _text + currentHealth;
    }

    public void Damage(float damage)
    {
        //Sets the currentHealth and rounds it up.
        //Updates the text.
        currentHealth = currentHealth + Mathf.Round(damage);

        if (currentHealth <= 0) //if the currenthealth is lower than 0 display a zero.
        {
            healthText.text = _text + 0;
        }
        else //as long the current health is more then 0, update the text.
        {
            healthText.text = _text + currentHealth;
        }
    }

}
