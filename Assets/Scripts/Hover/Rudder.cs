using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudder : MonoBehaviour
{
    public float turnForce;

    public void TurnForce(Rigidbody rb, float i)
    {
        float targetForce = turnForce * i;
        rb.AddForceAtPosition(-transform.forward * targetForce, transform.position);
        Debug.DrawRay(transform.position, -transform.forward, Color.red, 0.1f);
    }
}
