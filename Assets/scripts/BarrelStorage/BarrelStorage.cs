using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelStorage : MonoBehaviour
{
    [SerializeField]
    private int health;

    public GameObject explosion;

    private void Start()
    {
        ObjectiveManager.Instance.RegisterBarrelStorage(this.gameObject);
    }

    private void Update()
    {
        if (health <= 0)
        {
            Explode();
        }
    }

    public void SubtractDemage(int amount)
    {
        health -= amount;
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        explosion.transform.parent = null;
        ObjectiveManager.Instance.RevmoveBarrelStorage(this.gameObject);
        Destroy(this.gameObject, 0.1f);
    }
}
