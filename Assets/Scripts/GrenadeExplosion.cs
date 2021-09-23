using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    public Vector3 collision;
    private float explosionRadius = 5f;
    public int secconds = 5;

    public GameObject grenadeFX;

    private void Awake()
    {
        StartCoroutine(Timer());
    }

    //Waits the amount of seconds that is given before executing the follow function.
    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(secconds);
        StartCoroutine(Explode());
    }

    //Handles the explosion raycast and animation.
    private IEnumerator Explode()
    {
        //Plays the explosion audio.
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        //spawning the grenadeFX with the right rotation.
        GameObject FX = Instantiate(grenadeFX, transform.position,Quaternion.Euler (-90,0,0));

        //Creating the raycast to use for the explosion.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                //Kill player.
            }
        }

        //This is if the explosion happend, the granade, that is basicly gone, is not visable anymore.
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSecondsRealtime(4.3f);

        //Destroys gameobject.
        Destroy(gameObject);
        Destroy(FX);
    }

    //Editor only.
    private void OnDrawGizmos()
    {
        //This updates the position of the radius in the editor.
        collision = gameObject.transform.localPosition;

        //Draws the lines red and as an sphere.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, explosionRadius);
    }
}
