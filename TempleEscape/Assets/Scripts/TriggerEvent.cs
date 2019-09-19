using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    private ITriggerable _triggerableObject;

    private void OnTriggerEnter(Collider other)
    {
        _triggerableObject = this.transform.GetChild(0).GetComponent<ITriggerable>();
        _triggerableObject.Triggerd();
    }
}
