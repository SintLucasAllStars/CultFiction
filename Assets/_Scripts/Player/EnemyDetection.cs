using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();

    public PlayerCombat pc;

    private void Start()
    {
        pc = GameObject.FindObjectOfType<PlayerCombat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemys.Add(other.gameObject);
            pc.FoundTarget();
            other.gameObject.GetComponent<EnemyBehaviour>().lockOn.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            pc.LoseTarget();
            enemys.Remove(other.gameObject);
            other.gameObject.GetComponent<EnemyBehaviour>().lockOn.SetActive(false);
        }
    }
}
