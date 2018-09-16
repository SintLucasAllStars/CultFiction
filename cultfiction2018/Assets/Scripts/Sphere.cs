using System.Collections;
using System.Collections.Generic;
using MathExt;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Vector3[] Locations;
    public float ScaleFactor;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TheNeedle"))
        {
            transform.localPosition = Locations.GetRandom_Array();
            transform.localScale = transform.localScale -= new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
                ;
        }
    }
}
