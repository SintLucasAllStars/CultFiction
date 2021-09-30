using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManger : MonoBehaviour {
    [HideInInspector]
    public int Score;
    [HideInInspector]
    public float speed = 1;

    public GameObject pipe;

    private void Start() {
        speed = 1;
        StartCoroutine(Wank());
    }

    public void ScoreIncrement() {
        Score++;
        speed *= 1.01f;
        print(speed);
    }

    public void DeathState() {
        print("You're dead");
        Time.timeScale = 0;
    }

    IEnumerator Wank() {
        for (int i = 0; i < Mathf.Infinity; i++) {
            SpawnPipe();
            yield return new WaitForSeconds(1);
        }
    }

    void SpawnPipe() {
        Instantiate(pipe, transform.position + new Vector3(10, Random.Range(-2.5f,2.5f)), Quaternion.identity, transform);
    }
}