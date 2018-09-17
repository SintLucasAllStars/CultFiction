using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] Muzzle muzzleFlash;

    [Header("Weapon Settings")]
    public int ammo;
    [SerializeField] protected int maxAmmo;
    public int ammoStockPile;
    int maxAmmoStockPile;
    [SerializeField] protected float reloadTime;

    [SerializeField] protected float fireDistance = 1000;

    [Header("Weapon Damage")]
    [SerializeField] int defaultDamage;
    [SerializeField] int headShotDamage;
    [SerializeField] int armLegDamage;
    [SerializeField] float shootDelay;
    public bool isAutomatic = false;

    [Header("Weapon Recoil")]
    public Recoil recoil;

    [Header("Aim Settings")]
    [SerializeField] Vector3 aimPosition;
    [SerializeField] Vector3 aimRotation;
    [SerializeField] float fieldOfView;

    public bool isAming;

    Quaternion defaultRotation;
    Vector3 defaultPosition;
    float fieldOfViewDefault;

    Transform cameraTransform;
    float currentResetTime;
    protected bool canFire = true;

    public void Awake()
    {
        Debug.Log("Set");
        fieldOfViewDefault = Camera.main.fieldOfView;
        defaultPosition = transform.localPosition;
        defaultRotation = transform.localRotation;

        maxAmmoStockPile = ammoStockPile;
    }

    private void Update()
    {
        if(!canFire && Time.time > currentResetTime)
            canFire = true;
    }

    public virtual void Fire()
    {
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
            currentResetTime = Time.time + shootDelay;
        }
        else
        {
            Debug.Log("Reload");
            Reload();
        }
    }

    int GetDamge(string tag)
    {
        switch(tag)
        {
            case "Head":
                return headShotDamage;
            case "Body":
                return defaultDamage;
            case "Arm":
            case "Leg":
                return armLegDamage;
        }

        return defaultDamage;
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

        Vector3 forward = recoil.GetRecoil(cameraTransform.forward, isAming);
        Debug.DrawRay(cameraTransform.transform.position, forward, Color.red, 20);
        if(Physics.Raycast(cameraTransform.transform.position, forward, out hit, fireDistance))
        {
            return true;
        }
        return false;
    }

    public virtual void Reload()
    {
        if(ammo == maxAmmo || ammoStockPile == 0)
            return;

        Aim(false);

        currentResetTime = Time.time + reloadTime;
        canFire = false;

        if(ammo > 0)
        {
            ammoStockPile -= maxAmmo - ammo;
            Debug.Log("reloaded" + " " + (maxAmmo - ammo).ToString());
            ammo = maxAmmo;
        }
        else if(ammoStockPile >= maxAmmo)
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

    public virtual void Aim(bool aimDownSight)
    {
        isAming = aimDownSight;
        Camera.main.fieldOfView = (aimDownSight) ? fieldOfView: fieldOfViewDefault;
        transform.localPosition = (aimDownSight) ? aimPosition : defaultPosition;
        transform.localRotation = (aimDownSight) ? Quaternion.Euler(aimRotation) : defaultRotation;
    }

    protected void PlayAudioSource(AudioSource audioSource)
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }

    public virtual void Toggle(bool active)
    {
        gameObject.SetActive(active);

    }

    public void FillAmmo()
    {
        ammoStockPile = maxAmmoStockPile;
    }
}
