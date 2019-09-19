using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    public NavMeshAgent agent;
    protected InterfaceAI interfaceAI;

    protected Transform baseTarget; //The target that the object will attack.
    protected Transform baseShootDirection; //The direction where the object will shoot from.
    protected GameObject baseBulletObject; //The projectile that the object shoots.

    protected int baseId;

    protected float baseSpeed;
    protected int baseHealth;

    protected float baseWeaponSpeed;
    protected int baseDamage;
    protected int baseRange;
    protected int baseAccuracy; //Will be based on percentage. Normal = 50;

    protected int currentHealth;

    public virtual void CreateUnit(int id, float speed, int health, float weaponSpeed, int damage, int range, int accuracy)
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        interfaceAI = gameObject.GetComponent<InterfaceAI>();

        baseId = id;
        baseSpeed = speed;
        baseHealth = health;
        currentHealth = baseHealth;
        baseWeaponSpeed = weaponSpeed;
        baseDamage = damage;
        baseRange = range;
        baseAccuracy = accuracy;

        interfaceAI.SetSlider(currentHealth);
        agent.speed = baseSpeed;
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void Shoot(Transform target) //Shoots the baseTarget. 
    {
        int hitAccuracy = Random.Range(1, 101);
        if (hitAccuracy + baseAccuracy > 99)
        {
            target.GetComponent<BaseAI>().Hit(baseDamage);
            GameObject bullet = Instantiate(baseBulletObject);
        }
        else
        {
            GameObject bullet = Instantiate(baseBulletObject);
            Destroy(bullet, 5);
        }
    }

    public void Aim(Transform target)
    {
        Vector3 lookRotation = new Vector3 (target.position.x, transform.position.y, target.position.z);

        transform.LookAt(lookRotation);
    }


    public void Hit(int damage)
    {
        if (currentHealth -  damage < 1)
        {
            Death();
        }
        currentHealth = currentHealth - damage;
        interfaceAI.UpdateSlider(currentHealth);
    }

    public virtual void Death()
    {

    }


    /// <summary>
    /// Detects other AI through Overlapsphere. LayerMask 8 is used for Player AI. LayerMask 9 is used for Enemies.
    /// </summary>
    /// <param name="layerMask"></param>
    protected void SeeEnemy(int layerMask)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, baseRange);

        int i = 0;
        while (i < hitColliders.Length)
        {
            if (!IsObscured(hitColliders[i].transform))
            {
                baseTarget = hitColliders[i].transform;
                i = hitColliders.Length;
                Debug.Log("Targetted");
            }
            i++;
        }
    }

    protected bool IsObscured(Transform objectTarget)
    {
        if (Physics.Raycast(transform.position, objectTarget.position, out RaycastHit hit))
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject.CompareTag("AI"))
            {
                return false;
            }
        }
        return true;
    }
}