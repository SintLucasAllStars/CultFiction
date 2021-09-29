using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private bool Armed;
    private bool triggered;
    public float explosionTimer;
    public float explosionRadius;
    public GameObject explosionEffect;

    // Update is called once per frame
    void Update()
    {
        if (Armed)
        {
            Collider[] otherplanes = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider item in otherplanes)
            {
                if (item.CompareTag("AirPlane"))
                {
                    Armed = false;
                    triggered = true;
                }
            }
        }
        else if (triggered)
        {
            explosionTimer -= Time.deltaTime;
        }
        
        if (explosionTimer <= 0)
        {
            triggered = false;
            Explode();
        }


        if(transform.position.y <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 rndCircle = new Vector3(Random.insideUnitCircle.x * explosionRadius, Random.insideUnitCircle.y * explosionRadius) + transform.position;
            GameObject effect = Instantiate(explosionEffect, rndCircle, Quaternion.identity);
            Destroy(effect, .5f);
        }

        Collider[] otherplanes = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider item in otherplanes)
        {
            if (item.CompareTag("AirPlane"))
            {
                item.GetComponent<PlaneBehaviour>().Crash();
            }
        }

        Destroy(gameObject);
    }

    public void ArmGolfBallGrenade()
    {
        Armed = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
