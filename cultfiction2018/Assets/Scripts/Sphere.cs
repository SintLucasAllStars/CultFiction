using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour 
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("TheNeedle"))
        {
            GameManager.Instance.WinGame();
        }
    }
}
