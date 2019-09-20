using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject towerInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0f));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0f));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector2(0f, speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector2(0f, -speed * Time.deltaTime));
        }
    }
}
