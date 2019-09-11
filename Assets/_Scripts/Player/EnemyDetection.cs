using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : Ship
{
    public List<GameObject> enemys = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemys.Add(other.gameObject);
            other.gameObject.GetComponent<EnemyBehaviour>().lockOn.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemys.Remove(other.gameObject);
            other.gameObject.GetComponent<EnemyBehaviour>().lockOn.SetActive(false);
        }
    }
}
