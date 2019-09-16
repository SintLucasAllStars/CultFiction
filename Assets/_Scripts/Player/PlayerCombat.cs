using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool hasTarget;
    private Transform target;
    private bool tryingToLock;

    private EnemyDetection ed;
    private PlayerUI pu;

    [Header("Weapons")]
    public bool reloading;
    public GameObject minigun;
    public Transform missileSpawn1;
    public Transform missileSpawn2;
    public GameObject missilePrefab;

    void Start()
    {
        ed = GameObject.FindObjectOfType<EnemyDetection>();
        pu = GameObject.FindObjectOfType<PlayerUI>();
        StartCoroutine("ResetUI");
    }

    private void Update()
    {
        if (hasTarget && Input.GetKeyDown(KeyCode.F) && reloading == false)
        {
            GameObject missle1 = Instantiate(missilePrefab, missileSpawn1.position, missileSpawn2.rotation);
            missle1.GetComponent<TargetRocket>().target = target;
            missle1.GetComponent<TargetRocket>().enemyRocket = false;
            GameObject missle2 = Instantiate(missilePrefab, missileSpawn2.position, missileSpawn2.rotation);
            missle2.GetComponent<TargetRocket>().target = target;
            missle2.GetComponent<TargetRocket>().enemyRocket = false;

            reloading = true;
            Reload();
        }
    }

    public void FoundTarget()
    {
        tryingToLock = true;
        target = ed.enemys[0].transform;
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

    void Reload()
    {
        pu.StartReloadTimer();
    }

}
