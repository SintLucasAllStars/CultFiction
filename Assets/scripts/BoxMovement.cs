using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour {
    // Flap mechanics based on rigid body
    private Rigidbody2D rb;
    public Vector2 force = new Vector2(0, 10);

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        //Flap();
    }
    
    private void Update() {
        //flap detection
        if (Input.GetButtonDown("Jump")) {
            Flap();
        }
    }

    private void Flap() {
        rb.velocity = Vector2.zero;
        rb.AddForce(force);
    }
}