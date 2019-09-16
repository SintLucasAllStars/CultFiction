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
    bool reloading = false;

    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerFlyController>().transform;
    }
    
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) < shootDistance && reloading == false)
        {
            Shoot();
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
