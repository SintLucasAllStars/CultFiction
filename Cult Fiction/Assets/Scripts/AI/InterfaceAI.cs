using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceAI : MonoBehaviour
{
    public Transform canvas;

    private Slider healthBar;

    // Update is called once per frame
    private void Update()
    {
        canvas.LookAt(Camera.main.transform);
    }

    public void SetSlider(int maxHealth)
    {
        healthBar = transform.Find("Canvas/HealthBar").GetComponent<Slider>();
        healthBar.minValue = 0;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void UpdateSlider(int currentHealth)
    {
        healthBar.value = currentHealth;
    }
}