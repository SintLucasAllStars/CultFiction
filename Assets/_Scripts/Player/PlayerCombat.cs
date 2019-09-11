using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool hasTarget;
    private bool tryingToLock;

    private EnemyDetection ed;
    private PlayerUI pu;

    [Header("Weapons")]
    public GameObject minigun;

    void Start()
    {
        ed = GameObject.FindObjectOfType<EnemyDetection>();
        pu = GameObject.FindObjectOfType<PlayerUI>();
        StartCoroutine("ResetUI");
    }

    private void Update()
    {
        if (hasTarget && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Shoot missles");
        }
    }

    public void FoundTarget()
    {
        tryingToLock = true;
        StartCoroutine("LockOn");
        StopCoroutine("ResetUI");
        pu.targetText.text = "Trying To Lockon";
    }

    public void LoseTarget()
    {
        StopCoroutine("LockOn");
        StartCoroutine("ResetUI");
        hasTarget = false;
        pu.targetText.text = "Lost target";
        Debug.Log("Failed lost target");
    }

    IEnumerator LockOn()
    {
        yield return new WaitForSeconds(1.5f);
        hasTarget = true;
        pu.targetText.text = "Target Found";
        Debug.Log("Target Assigned can shoot");
    }

    IEnumerator ResetUI()
    {
        yield return new WaitForSeconds(1.5f);
        pu.targetText.text = "No Target";
    }

}
