using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject m_player;
    
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
}
