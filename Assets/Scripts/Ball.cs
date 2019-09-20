using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D rb;
    public float sandDrag;

    public Vector2 teePos;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        GameManager.instance.isComplete = false;
    }

    #region Collision mechanics

    void OnCollisionEnter2D(Collision2D cld) {
        if (cld.gameObject.tag == "Flagpole")
        {
            rb.velocity = Vector2.zero;
            GameManager.instance.LvComplete();
        }
    }

    void OnTriggerEnter2D(Collider2D cld) {
        if (cld.gameObject.tag == "Sandpit")
        {
            rb.drag *= sandDrag;
        }
    }

    void OnTriggerExit2D(Collider2D cld) {
        if (cld.gameObject.tag == "Sandpit")
        {
            rb.drag /= sandDrag;
        }
    }

    void OnTriggerStay2D(Collider2D cld) {
        if (cld.gameObject.tag == "Water")
        {
            rb.velocity = Vector2.zero;
            gameObject.transform.position = teePos;
        }
    }

    #endregion

}