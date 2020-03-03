using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Block : MonoBehaviour { 
Rigidbody2D rb2d;
  


void Start()
{
     rb2d = GetComponent<Rigidbody2D>();
}

void OnCollisionEnter2D(Collision2D col)
{
    if (col.gameObject.name.Equals ("Player")) {
        Invoke ("DropPlatform", 0f);
        Destroy (gameObject, 0.01f);
    }
 }


void DropPlatform()
{
    rb2d.isKinematic = false;
    }
}

