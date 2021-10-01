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
    private ParticleSystem guideLine;
    private AudioSource explosionSFX;

    private void Start()
    {
        guideLine = transform.GetChild(0).GetComponent<ParticleSystem>();
        explosionSFX = gameObject.GetComponent<AudioSource>();
    }

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
            explosionTimer = 1;
            explosionSFX.Play();
            Explode();
        }


        if (transform.position.y <= 0)
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
            Destroy(effect, 1f);
        }
        Collider[] otherplanes = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider item in otherplanes)
        {
            if (item.CompareTag("AirPlane"))
            {
                item.GetComponent<PlaneBehaviour>().Crash();
            }
        }
        Destroy(gameObject,5);
    }

    public void ArmGolfBallGrenade()
    {
        Armed = true;
    }

    public void UpdateGuideLine(float y, float z, float power)
    {
        if (!Armed)
        {
            var velocityOverLifetime = guideLine.velocityOverLifetime;
            var emission = guideLine.emission;
            emission.rateOverTime = 10 * power;
            velocityOverLifetime.y = y;
            velocityOverLifetime.z = z;
        }
    }

    public void DisableGuideLine()
    {
        guideLine.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
