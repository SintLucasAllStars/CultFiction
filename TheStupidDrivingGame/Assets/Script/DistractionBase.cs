using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionBase : MonoBehaviour
{
    public int foodPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouth"))
        {
            other.GetComponent<Player>().Eat(foodPoints);
            Destroy(this.gameObject);
        }
    }
}