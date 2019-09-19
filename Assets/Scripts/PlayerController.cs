using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    public float boostSpeed = 20;

    public AudioSource speedSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speedSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "End")
        {
            transform.position = new Vector3(21.55f, 0.91f, -5f);
        }

        if (col.gameObject.tag == "End2")
        {
            transform.position = new Vector3(36.456f, 0.91f, -5.22f);
        }

        if (col.gameObject.tag == "End3")
        {
            transform.position = new Vector3(47.565f, 0.99f, -6.605f);
        }

        if (col.gameObject.tag == "End4")
        {
            transform.position = new Vector3(60.9f, 0.99f, -5.2f);
        }

        if (col.gameObject.tag == "Boost")
        {
            rb.velocity = (col.transform.forward * 8);

            speedSound.Play();
        }

        if (col.gameObject.tag == "Level3Fail")
        {
            transform.position = new Vector3(36.456f, 0.91f, -5.22f);
            transform.rotation = Quaternion.identity;
        }
    }
}
