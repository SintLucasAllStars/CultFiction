using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private float force = 200;
    private bool going;
    void Start()
    {
        going = false;
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.localScale = new Vector3(
            Random.Range(0.5f, 1.5f),
            Random.Range(0.5f, 1.5f),
            Random.Range(0.5f, 1.5f));
        going = true;
    }
    void Update()
    {
        gameObject.transform.LookAt(player.transform);
        if (going == true)
        {
            Debug.Log("test");
            rb.AddForce(transform.forward * 30);
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x - 0.001f,
                gameObject.transform.localScale.y - 0.001f,
                gameObject.transform.localScale.z - 0.001f
                );
        }
        // special if statement OR 
        if(gameObject.transform.localScale.x < 0 || gameObject.transform.localScale.z < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
