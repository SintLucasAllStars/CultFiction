using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private GameObject m_EnemyPrefab;
    
    public int m_EnemiesKilled;
    [SerializeField] private Text m_KillField;
    [SerializeField] private Text m_GoldLeftField;

    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_SfxStolen;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.instance.LevelChange("Quit");
        }
    }

    public void GetDeadEnemies()
    {
        GameObject[] m_DeadEnemies;
        m_DeadEnemies = GameObject.FindGameObjectsWithTag("Dead");
        if (m_DeadEnemies.Length > 5)
        {
            Destroy(m_DeadEnemies[0]);
        }
    }

    public void GetTreasures()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.clip = m_SfxStolen;
        m_AudioSource.Play();
        
        GameObject[] m_Treasures;
        m_Treasures = GameObject.FindGameObjectsWithTag("Treasure");
        m_GoldLeftField.text = m_Treasures.Length.ToString();
        if (m_Treasures.Length < 1)
        {
            LevelManager.instance.LevelChange("GameOver");
        }
    }

    public void EnemySpawn(Vector3 pos)
    {
        m_EnemiesKilled++;
        if (m_EnemiesKilled <= 99999)
        {
            m_KillField.text = m_EnemiesKilled.ToString();
        }
        else
        {
            m_KillField.text = "A Lot";
        }

        Instantiate(m_EnemyPrefab, pos, Quaternion.identity);
    }

    public void TogglePlayer(bool state)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = !state;
            player.GetComponentInChildren<Shoot>().enabled = state;
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = state;
        }
    }

    public void GameReset()
    {
        m_EnemiesKilled = 0;
        
        GameObject[] m_DeadEnemies;
        m_DeadEnemies = GameObject.FindGameObjectsWithTag("Dead");
        for (int i = 0; i < m_DeadEnemies.Length; i++)
        {
            Destroy(m_DeadEnemies[i].gameObject);
        }
        
    }

}
