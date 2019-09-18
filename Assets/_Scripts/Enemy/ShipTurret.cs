using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTurret : MonoBehaviour
{

    [Header("References")]
    public Transform barrel;
    public GameObject missilePrefab;
    public LockOnLookat rotation;

    [Header("Turret Stats")]
    public float reloadTime;
    public LayerMask scanHitable;
    public int turretRange;
    
    private bool reloading = false;
    private Transform target;
    
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
        if (PlayerInSight() && reloading == false && eb.health > 0)
        {
            Shoot();
        }
        if (eb.health > 1)
        {
            rotation.RotateToPlayer();
        }
    }

    bool PlayerInSight()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, (target.position - transform.position), out hit, Mathf.Infinity, scanHitable))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("I see Ship");
                return true;
            }
        }
        return false;
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
