using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject playersHead;

    private void Start()
    {
        playersHead = GameObject.Find("Player Camera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print(playersHead.name);
            playersHead.transform.SetParent(null);
            playersHead.GetComponent<Rigidbody>().isKinematic = false;
            playersHead.GetComponent<Collider>().isTrigger = false;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            StartCoroutine(RespawnAvailable());
        }
    }
    IEnumerator RespawnAvailable()
    {
        yield return new WaitForSeconds(6);
        GameObject.Find("Player").GetComponent<CharacterController>().isDead = true;
    }
}
