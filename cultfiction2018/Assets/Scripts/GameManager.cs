using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject SyringePrefab;
    public HandMovement PlayerController;
    public ParticleSystem BloodEffect;
    public Slider HealthSlider;
    public Text PanelText;
    public GameObject Panel;

    [SerializeField] private float _damagePerSyringe = .001f;

    private int _amountOfSyringesIn = 0;
    private bool _uiActive;
    

    public void GetNewSyringe()
    {
        Instantiate(SyringePrefab, PlayerController.transform);
    }

    public void BodyHit(Vector3 spawnPosition)
    {
        SpawnBlood(spawnPosition);
        _amountOfSyringesIn++;
    }

    private void SpawnBlood(Vector3 spawnPosition)
    {
        Instantiate(BloodEffect, spawnPosition,Quaternion.Euler(-90,0,0));
        
    }
    

    public void WinGame()
    {
        PanelText.text = "You win!";
        Panel.gameObject.SetActive(true);
        _uiActive = true;

    }

    public void LoseGame()
    {
        PanelText.text = "You lose!";
        Panel.gameObject.SetActive(true);
        _uiActive = true;

    }

    public bool UIactive()
    {
        return _uiActive;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
    

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    

    private void Update()
    {
        if (!UIactive())
        {
            HealthSlider.value -= _amountOfSyringesIn * _damagePerSyringe * Time.deltaTime;
            if (HealthSlider.value <= 0)
            {
                LoseGame();
            }
            
        }

    }
}
