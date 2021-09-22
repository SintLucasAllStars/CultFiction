using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeExplosion : MonoBehaviour
{
    public Vector3 collision;
    public float explosionRadius = 0.2f;
    public int secconds = 5;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    //waits the amount of seconds that is given before executing the follow function.
    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(secconds);
        StartCoroutine(Explode());
    }

    //handles the explosion raycast and animation.
    private IEnumerator Explode()
    {
        //plays the explosion audio.
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        //plays the particle system.
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();

        //Creating the raycast to use for the explosion.
        var ray = new Ray(transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.Log(hit.transform.gameObject);
            collision = hit.point;
            Debug.Log("You died");
        }

        //This is if the explosion happend, the granade, that is basicly gone, is not visable anymore.
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSecondsRealtime(4.3f);

        //destroys gameobject
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        //this updates the position of the radius in the editor
        collision = gameObject.transform.localPosition;

        //Draws the lines red and as an sphere.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, radius: explosionRadius);
    }
}
