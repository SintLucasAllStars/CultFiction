using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManger : MonoBehaviour {
    [HideInInspector]
    public int Score;
    [HideInInspector]
    public float speed;
    public Text scoreText;
    public GameObject pipe;
    private bool gameStopped;

    private void Start() {
        speed = 1;
        StartCoroutine(PipeLoop());
        scoreText.text = $"{Score}";
    }

    public void ScoreIncrement() {
        Score++;
        speed *= 1.02f;
        print(speed);
        scoreText.text = $"{Score}";
    }

    public void DeathState() {
        print("You're dead");
        Time.timeScale = 0;
        gameStopped = true;
    }

    private void Update() {
        if (gameStopped && Input.GetButtonDown("Jump")) {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator PipeLoop() {
        for (int i = 0; i < Mathf.Infinity; i++) {
            SpawnPipe();
            yield return new WaitForSeconds(1/speed);
        }
    }

    void SpawnPipe() {
        Instantiate(pipe, transform.position + new Vector3(10, Random.Range(-2.5f,2.5f)), Quaternion.identity, transform);
    }
}