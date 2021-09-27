using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDirector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>() != null) GameManager.gameManager.CollisionCaller(gameObject);
    }
}
