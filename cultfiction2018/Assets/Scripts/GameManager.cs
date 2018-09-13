using System.Collections;
using System.Collections.Generic;
using LogIn;
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

    private bool _heartBeatUpgraded;
    

    public void GetNewSyringe()
    {
        Instantiate(SyringePrefab, PlayerController.transform);
        AddSyringeCount();
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
        Time.timeScale = 0;
        _uiActive = true;

    }

    public void LoseGame()
    {
        PanelText.text = "You lose!";
        Panel.gameObject.SetActive(true);
        Time.timeScale = 0;
        _uiActive = true;

    }

    public void EscapeHit()
    {
        if (_uiActive)
        {
            Panel.gameObject.SetActive(false);
            Time.timeScale = 1;
            _uiActive = false;
            
        }
        else
        {
            PanelText.text = "";
            Panel.gameObject.SetActive(true);
            Time.timeScale = 1;
            _uiActive = true;
        }

    }

    public bool UIactive()
    {
        return _uiActive;
    }

    public void AddSyringeCount()
    {
        DBmanager.Score++;
    }

    public void Restart()
    {
        CallSaveData("MainScene");
      
    }
    

    public void BackToMenu()
    {
        CallSaveData("MainMenu");
       
    }

    public void CallSaveData(string sceneName)
    {
        StartCoroutine(IESavePlayerData(sceneName));
    }

    private IEnumerator IESavePlayerData(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBmanager.Username);
        form.AddField("score", DBmanager.Score);
        
        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Game Saved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }
        
        DBmanager.LogOut();
        SceneManager.LoadScene(sceneName);
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

            if (HealthSlider.value < 30 && !_heartBeatUpgraded)
            {
                SoundManager.Instance.IncreaseHeartBeat();
                _heartBeatUpgraded = true;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeHit();
        }




    }
}
