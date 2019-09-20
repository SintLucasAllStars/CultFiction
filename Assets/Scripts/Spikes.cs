using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetChild(0).SetParent(null);
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GameObject.Find("Player").GetComponent<CharacterController>().isDead = true;
            StartCoroutine(RespawnAvailable());
        }
    }
    IEnumerator RespawnAvailable()
    {
        yield return new WaitForSeconds(8);
        GameObject.Find("Player").GetComponent<CharacterController>().isDead = true;
    }
}
