using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject boom;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }
}
