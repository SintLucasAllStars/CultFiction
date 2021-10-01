using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public float currentDist;
    public float curForce;
    //minimal = max thrust!
    public float minDist, maxDist, thrustForce;
    private RaycastHit hit;

    public void ThrustForce(Rigidbody rb)
    {
        GroundDetection();
        rb.AddForceAtPosition(transform.forward * thrustForce, transform.position);
        Debug.DrawRay(transform.position, -transform.forward, Color.red, 0.1f);
    }

    void GroundDetection()
    {
        Vector3 player = transform.position;

        Physics.SphereCast(player, player.y / 2, -transform.forward, out hit);
        currentDist = hit.distance;

        float distRemap = (hit.distance - maxDist) / (minDist - maxDist);
        float distReclamp = Mathf.Clamp(distRemap, 0 ,1);
        curForce = distReclamp * thrustForce;

    }
}
