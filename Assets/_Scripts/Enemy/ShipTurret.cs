using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTurret : MonoBehaviour
{
    private Transform target;
    public Transform barrel;
    public GameObject missilePrefab;
    public float shootDistance;
    public float reloadTime;
    public LockOnLookat rotation;
    bool reloading = false;

    EnemyBehaviour eb;

    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerFlyController>().transform;
        eb = GetComponent<EnemyBehaviour>();
    }
    
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) < shootDistance && reloading == false && eb.health > 0)
        {
            Shoot();
        }
        if (eb.health > 1)
        {
            rotation.RotateToPlayer();
        }
    }

    void Shoot()
    {
        GameObject missle = Instantiate(missilePrefab, barrel.position, barrel.rotation);
        missle.GetComponent<TargetRocket>().target = target;
        missle.GetComponent<TargetRocket>().enemyRocket = true;
        StartCoroutine(ReloadTimer());
    }
    
    IEnumerator ReloadTimer()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }

}
