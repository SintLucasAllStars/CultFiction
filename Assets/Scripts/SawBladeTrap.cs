using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeTrap : MonoBehaviour
{
    public GameObject saw;
    public bool trapActive;
    public float rotSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trapActive = true;
        }
    }
    private void FixedUpdate()
    {
        if (trapActive)
        {
            saw.transform.Rotate(0, rotSpeed, 0);
        }
    }
}
