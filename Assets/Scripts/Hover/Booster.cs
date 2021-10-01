using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float boostForce;

    public void BoostForce(Rigidbody rb, float i)
    {
        //Slowly transition from curspeed to full boosting speed
        //Check what side the force is supposed to be towards
        float targetForce = boostForce * i;
        rb.AddForceAtPosition(-transform.forward * targetForce, transform.position);
        Debug.DrawRay(transform.position, -transform.forward, Color.red, 0.1f);
    }
}
