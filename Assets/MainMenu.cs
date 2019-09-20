using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    bool multiplayer;
    public TextMeshProUGUI players;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            menu.instance.playmode = playModes.Singleplayer;
            players.text = "Playmode : Singleplayer";
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            menu.instance.playmode = playModes.Multiplayer;
            players.text = "Playmode : Multiplayer";
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene(1);
        }
    }
}
