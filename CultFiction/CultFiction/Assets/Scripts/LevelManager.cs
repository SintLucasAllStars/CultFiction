using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static SceneManager instance;
    ScoreManager scoreManager;
	// Use this for initialization
	void Awake () {
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            scoreManager.pins.Clear();
        }
	}
    public void SceneSwitch(int scene)
    {
        scoreManager.Clean();
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
    public void Close()
    {
        Application.Quit();
    }
}
