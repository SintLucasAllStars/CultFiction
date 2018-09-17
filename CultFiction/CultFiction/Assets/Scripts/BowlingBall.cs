using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    public AudioVisualizer av;
    ScoreManager scoreManager;
    Rigidbody rb;
    public bool isThrowing;
    public float power;
    bool restartText = false;
    float timer = 0.5f;
    //float velocity;
    float speedX = 10f;
    float xMax = 8.75f;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isThrowing)
        {
            float x = Input.GetAxis("Horizontal") * speedX * Time.deltaTime;
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -xMax, xMax);
            transform.position = pos;
            transform.Translate(x, 0, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector3.forward * power;
                isThrowing = true;
            }

        }
        else
        {

            Time.timeScale -= timer * Time.deltaTime;
            if (Time.timeScale <= 0.15f)
            {
                scoreManager.Tally();
                restartText = true;
            }
        }


    }

    private void OnGUI()
    {
        if (restartText)
        {
            GUI.color = Color.green;
            GUI.TextField(new Rect(10, 10, 200, 20), "Press R to restart");

        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Pin"))
        {
            Rigidbody rbPin = coll.gameObject.GetComponent<Rigidbody>();
            av.isHit = true;
            rb.velocity = coll.gameObject.transform.forward * power;
            rbPin.isKinematic = false;
            rbPin.useGravity = true;
            rbPin.AddForceAtPosition(Vector3.forward * power, coll.gameObject.transform.position);
        }
    }
}
