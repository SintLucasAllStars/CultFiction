using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player")) col.GetComponent<Health>().Hp--;
        else if (!col.CompareTag("Enemy")) Destroy(this.gameObject);
    }
}
