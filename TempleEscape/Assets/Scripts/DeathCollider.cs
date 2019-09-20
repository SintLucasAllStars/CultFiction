using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.OnDeath();
        TiggerManager.ResetTriggers();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent <Player>()) return;

        TiggerManager.ResetTriggers();
        Player.Instance.OnDeath();
    }
}
