using System;
using UnityEngine;

public class BoxMovement : MonoBehaviour {
    private Rigidbody2D rb;
    public GameManger gm;
    public Vector2 force = new Vector2(0, 10);

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManger>();
        Flap();
    }
    
    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            Flap();
        }
        if (transform.position.y >= 10 || transform.position.y <= -10)
            gm.DeathState();
    }

    private void Flap() {
        rb.velocity = Vector2.zero;
        rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("PipeSection"))
            gm.DeathState();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("ScoreIncrease"))
            gm.ScoreIncrement();
    }

}