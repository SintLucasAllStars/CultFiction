using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTrigger : MonoBehaviour
{
    public Gun interactableGun;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            interactableGun = other.gameObject.GetComponent<Gun>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactableGun = null;
    }
}
