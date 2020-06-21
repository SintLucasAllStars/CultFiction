using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class PlayerManager : MonoBehaviour
{
    public Slider healthBar;
    private HealthComponent health;
    [Inject] private FeedbackSystem _feedbackSystem;

    private void Start()
    {
        health = gameObject.GetComponent<HealthComponent>();
        healthBar.maxValue = health.maxHealth;
    }

    private void Update()
    {
        healthBar.value = health.currentHealth;

        if (health.currentHealth <= 20)
        {
            _feedbackSystem.PlayWarning("Low health", 5);
        }

        if (health.currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}