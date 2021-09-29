﻿using UnityEngine;

public class BoxMovement : MonoBehaviour {
    private Rigidbody2D rb;
    public GameManger gm;
    public Vector2 force = new Vector2(0, 10);

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManger>();
    }
    
    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            Flap();
        }
        gm.ScoreIncrement();
    }

    private void Flap() {
        rb.velocity = Vector2.zero;
        rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("PipeSection"))
            gm.DeathState();
    }
}