using System;
using UnityEngine;

public class PipeMovement : MonoBehaviour {
    private GameManger gm;
    private float baseSpeed = 10;

    private void Start() {
        gm = FindObjectOfType<GameManger>();
    }

    private void Update() {
        transform.Translate(baseSpeed * Vector2.left * gm.speed * Time.deltaTime);
        if (transform.position.x < -11) Destroy(gameObject);
    }
}
