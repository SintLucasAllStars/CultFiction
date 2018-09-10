using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] Muzzle muzzleFlash;

    [Header("Weapon Settings")]
    [SerializeField] protected int ammo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int ammoStockPile;
    [SerializeField] protected float reloadTime;

    [SerializeField] protected float fireDistance = 1000;

    [Header("Weapon Damage")]
    [SerializeField] int defaultDamage;
    [SerializeField] int headShotDamage;
    [SerializeField] int armLegDamage;
    [SerializeField] float shootDelay;
    public bool isAutomatic = false;

    Transform cameraTransform;
    float currentResetTime;
    bool canFire = true;

    private void Update()
    {
        if(!canFire && Time.time > currentResetTime)
            canFire = true;
    }

    public void Fire()
    {
        Debug.Log("Fire");
        if(!canFire)
            return;

        if(ammo > 0)
        {
            canFire = false;
            RaycastHit hit = new RaycastHit();
            if(RaycastForward(ref hit))
            {
                Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
                if(enemy != null)
                    enemy.Damage(GetDamge(hit.collider.tag));
                Debug.Log(enemy);
            }
            ammo--;
            MuzzleFlash();
            FireExtra();
            currentResetTime = Time.time + shootDelay;
        }
        else
            Reload();
    }

    int GetDamge(string tag)
    {
        switch(tag)
        {
            case "Head":
                Debug.Log("Boom Headshot");
                return headShotDamage;
            case "Body":
                Debug.Log("Body Damage");
                return defaultDamage;
            case "Arm":
            case "Leg":
                Debug.Log("I once was an adventurer like you but then I took a bullet to the leg or arm");
                return armLegDamage;
        }

        return defaultDamage;
    }

    protected virtual void FireExtra()
    {

    }


    void MuzzleFlash()
    {
        if(muzzleFlash != null)
            muzzleFlash.Enable();
    }

    bool RaycastForward(ref RaycastHit hit)
    {
        if(cameraTransform == null)
            cameraTransform = Camera.main.transform;
        Debug.DrawRay(cameraTransform.transform.position, cameraTransform.forward, Color.red, 20);
        if(Physics.Raycast(cameraTransform.transform.position, cameraTransform.forward, out hit, fireDistance))
        {
            return true;
        }
        return false;
    }

    protected void PlayAudioSource(AudioSource audioSource)
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void Reload()
    {
        if(ammo == maxAmmo || ammoStockPile == 0)
            return;
        currentResetTime = Time.time + reloadTime;
        canFire = false;
        ReloadAnimation();

        if(ammo > 0)
        {
            ammo = maxAmmo;
            ammoStockPile -= maxAmmo - ammo;
        }
        if(ammoStockPile >= maxAmmo)
        {
            ammo = maxAmmo;
            ammoStockPile -= maxAmmo;
        }
        else if(ammoStockPile < maxAmmo)
        {
            ammo = ammoStockPile;
            ammoStockPile = 0;
        }
        else
        {
            Debug.Log("Out of Ammo");
            return;
        }
    }


    protected virtual void ReloadAnimation()
    {

    }

    public virtual void Toggle(bool active)
    {
        gameObject.SetActive(active);

    }
}
