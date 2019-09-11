using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool hasTarget;
    private bool tryingToLock;

    private void Update()
    {
        if (hasTarget && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shoot");
        }
    }

    public void FoundTarget()
    {
        tryingToLock = true;
        StartCoroutine("LockOn");
        Debug.Log("Trying To Lock On...");
    }

    public void LoseTarget()
    {
        StopCoroutine("LockOn");
        hasTarget = false;
        Debug.Log("Failed lost target");
    }

    IEnumerator LockOn()
    {
        yield return new WaitForSeconds(1.5f);
        hasTarget = true;
        Debug.Log("Target Assigned can shoot");
    }

}
