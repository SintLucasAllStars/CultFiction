using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int m_EnemiesKilled;
    public GameObject m_player;
    [SerializeField] private GameObject m_EnemyPrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            GetPlayer();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void GetPlayer()
    {
        if (m_player == null)
        {
            m_player = GameObject.FindWithTag("Player");
        }
    }

    public void GetTreasures()
    {
        GameObject[] m_Treasures;
        m_Treasures = GameObject.FindGameObjectsWithTag("Treasure");
        if (m_Treasures.Length < 1)
        {
            Debug.Log("You lose.");
        }
    }

    public void EnemySpawn(Vector3 pos)
    {
        m_EnemiesKilled++;
        Instantiate(m_EnemyPrefab, pos, Quaternion.identity);
    }
}
