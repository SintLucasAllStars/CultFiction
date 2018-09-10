using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] Muzzle muzzleFlash;

    [Header("Audio")]
    [SerializeField]
    AudioSource audioSource;

    [Header("Weapon Settings")]
    [SerializeField] protected int ammo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int ammoStockPile;

    [SerializeField] protected float fireDistance = 1000;

    [Header("Weapon Damage")]
    [SerializeField] int defaultDamage;
    [SerializeField] int headShotDamage;
    [SerializeField] int armLegDamage;

    Transform cameraTransform;

    public void Fire()
    {
        RaycastHit hit = new RaycastHit();
        if(RaycastForward(ref hit))
        {
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
            if(enemy != null)
                enemy.Damage(GetDamge(hit.collider.tag));
            Debug.Log(enemy);
        }
        FireExtra();
        MuzzleFlash();
        ammo--;
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

    protected void PlayGunAudio(AudioClip currentClip)
    {
        if(audioSource != null)
        {
            if(audioSource.clip != currentClip)
                audioSource.clip = currentClip;
            audioSource.Play();
        }
    }

    public void Reload()
    {
        if(ammoStockPile > maxAmmo)
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
        ReloadAnimation();
    }

    protected virtual void ReloadAnimation()
    {

    }

    public virtual void Toggle(bool active)
    {
        gameObject.SetActive(active);

    }
}
