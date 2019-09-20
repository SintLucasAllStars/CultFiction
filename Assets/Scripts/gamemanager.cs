using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum playModes { Singleplayer, Multiplayer }
public class gamemanager : MonoBehaviour
{
    private gamemanager instance;
    public playModes playMode;
    public GameObject player;
    public List<GameObject> players;
    public Transform[]spawnPlace;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
    }
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

    public void StartGame()
    {
        players = new List<GameObject>();
        switch (playMode)
        {
            case playModes.Singleplayer:
                players.Add(Instantiate(player, spawnPlace[0].position, spawnPlace[0].rotation));
                break;
            case playModes.Multiplayer:
                for (int i = 0; i < 2; i++)
                {
                    players.Add(Instantiate(player,spawnPlace[i].position,spawnPlace[i].rotation));
                }
                Camera cam1 = players[0].GetComponentInChildren<Camera>();
                cam1.rect = new Rect(0f, 0f, 0.5f, 1f);
                cam1.cullingMask &= ~(1 << LayerMask.NameToLayer("Player2"));
                Camera cam2 = players[1].GetComponentInChildren<Camera>();
                players[1].GetComponentInChildren<CarController>().playerNum = 1;
                cam2.rect = new Rect(0.5f, 0f, 0.5f, 1f);
                cam2.cullingMask &= ~(1 << LayerMask.NameToLayer("Player1"));
                spawnPlace = new Transform[2];

                break;
        }
    }
}
