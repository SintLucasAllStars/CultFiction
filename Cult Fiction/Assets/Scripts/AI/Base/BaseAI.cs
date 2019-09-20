using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    public enum Behaviour {normal, attack}
    protected Behaviour behaviour;

    //Shooting and Aiming.
    public GameObject baseBulletObject; //The projectile that the object shoots.
    public Transform baseGunObject;
    public Transform baseShootDir; //The direction where the object will shoot from.
    protected Transform baseTarget; //The target that the object will attack.
    protected float TimestampShoot;

    //debug
    public float bulletSpeed;
    public float baseWeaponSpeed;

    //Scripts
    protected InterfaceAI interfaceAI;
    protected NavMeshAgent agent;


    //On unit creation
    protected int baseId;
    protected float baseSpeed;
    protected int baseHealth;

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
        GameObject bulletInstance;
        if (Time.time >= TimestampShoot)
        {
            int hitAccuracy = Random.Range(1, 101);
            Debug.Log(hitAccuracy);
            if (hitAccuracy + baseAccuracy >= 100)
            {
                bulletInstance = Instantiate(baseBulletObject, baseShootDir.transform.position, baseShootDir.transform.rotation) as GameObject;

                bulletInstance.GetComponent<Rigidbody>().AddForce(baseShootDir.forward * bulletSpeed);
                TimestampShoot = Time.time + baseWeaponSpeed;

                target.GetComponent<BaseAI>().Hit(baseDamage);

                if (baseTarget == null)
                {
                    behaviour = Behaviour.normal;
                }
            }
            else
            {
                Vector3 misFireDirection = new Vector3(baseShootDir.rotation.eulerAngles.x + Random.Range(10, 60), baseShootDir.rotation.eulerAngles.y + Random.Range(10, 60), baseShootDir.rotation.eulerAngles.z + Random.Range(10, 60));
                bulletInstance = Instantiate(baseBulletObject, baseShootDir.transform.position, baseShootDir.transform.rotation) as GameObject;

                Vector3 misfire = new Vector3(baseShootDir.forward.x, baseShootDir.forward.y + Random.Range(-0.5f, 0.5f), baseShootDir.forward.x + Random.Range(-0.5f, 0.5f));

                bulletInstance.GetComponent<Rigidbody>().AddForce(misfire * bulletSpeed);
                TimestampShoot = Time.time + baseWeaponSpeed;
            }
            TimestampShoot = Time.time + baseWeaponSpeed;
        }
    }

    public void Aim(Transform target)
    {
        Vector3 lookRotation = new Vector3 (target.position.x, transform.position.y, target.position.z);

        transform.LookAt(lookRotation);

        baseGunObject.LookAt(target);
    }

    protected void NoTarget()
    {
        behaviour = Behaviour.normal;
        baseTarget = null;
        baseGunObject.localPosition = Vector3.zero;
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
        Object.Destroy(gameObject);
    }


    /// <summary>
    /// Detects other AI through Overlapsphere. LayerMask 8 is used for Player AI. LayerMask 9 is used for Enemies.
    /// </summary>
    /// <param name="layerMask"></param>
    protected void SeeEnemy(int layerMask)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, baseRange, layerMask);

        int i = 0;
        while (i < hitColliders.Length)
        {
            Vector3 direction = transform.position - hitColliders[i].transform.position;

            if (!IsObscured(direction, layerMask))
            {
                baseTarget = hitColliders[i].transform;
                i = hitColliders.Length;
                behaviour = Behaviour.attack;
            }
            i++;
        }
    }

    protected bool IsObscured(Vector3 direction, int layerMask)
    {
        if (Physics.Raycast(transform.position, -direction, out RaycastHit hit, baseRange))
        {
            if (hit.collider.gameObject.CompareTag("AI"))
            {
                return false;
            }
        }
        return true;
    }
}