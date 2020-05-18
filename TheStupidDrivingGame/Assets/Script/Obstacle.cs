using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float sensitivity;
    public int collisionDamage = 1;

    void Update()
    {
        transform.Translate(Vector3.back / sensitivity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            other.GetComponent<Player>().Hit(collisionDamage);
        }
    }
}
