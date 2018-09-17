using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class ShipController : MonoBehaviour
{
    public Text healthText;
    public Vector3 resetSpawnPosition;

    public int playerHealth = 3;
    public float speed;
    public float tilt;
    public Boundary boundary;

    Rigidbody rb;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb = GetComponent<Rigidbody>();
        rb.velocity = movement * speed;

        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);
    }

    private void Start()
    {
        playerHealth = 3;
        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthText.text = "" + playerHealth;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            playerHealth--;
            UpdateHealth();

            if (playerHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
