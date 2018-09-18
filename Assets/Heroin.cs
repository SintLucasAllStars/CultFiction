using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heroin : MonoBehaviour
{


    [SerializeField]
    private int points = 3;
    float pointsFactor = 0.2f;

    [SerializeField]
    private float speed = 100;
    float speedFactor = 0.2f;

    [SerializeField]
    private int health = 5;
    float healthFactor = 0.15f;
    private int maxHealth;

    int position = 1;


    // Use this for initialization
    void Start()
    {
        float points = Gamecontroller.instance.Factor(this.points, pointsFactor);
        this.points = (int)points;

        speed = Gamecontroller.instance.Factor(speed, speedFactor);

        float health = Gamecontroller.instance.Factor(this.health, healthFactor);
        this.health = (int)health;
        maxHealth = this.health;

        transform.localPosition = Gamecontroller.instance.vainPosition[0];
    }

    public void Set(int points, float speed, int health)
    {
        this.points = points;
        this.speed = speed;
        this.health = health;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        Vector2 pos = Gamecontroller.instance.vainPosition[position];
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, pos, step);

        if ((Vector2)transform.localPosition == pos)
        {
            position++;
            if (position >= Gamecontroller.instance.vainPosition.Count)
            {
                Gamecontroller.instance.SubtractLife();
                Die();
            }
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Gamecontroller.instance.points += points;
            Die();
        }
    }

    public void Hit(int damage)
    {
        health -= damage;

        float cRestant = 1.0f / maxHealth * health;

        Color c = new Color(1, cRestant, cRestant);
        GetComponent<Image>().color = c;
        CheckHealth();
    }

    void Die()
    {
        UiManager.instance.UpdateUI();
        Gamecontroller.instance.heroin.Remove(this);
        Destroy(gameObject);
    }

}
