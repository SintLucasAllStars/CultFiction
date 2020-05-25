using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InCar : MonoBehaviour
{

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouth"))
        {
            OnEat();
        }
    }

    protected abstract void OnEat();
}
