using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] Muzzle muzzleFlash;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;


    [Header("Weapon Settings")]
    [SerializeField] protected int ammo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int ammoStockPile;

    [SerializeField] protected float fireDistance = 1000;

    Transform cameraTransform;

    public abstract void Fire();

    protected void MuzzleFlash()
    {
        if(muzzleFlash != null)
            muzzleFlash.Enable();
    }

    protected bool RaycastForward(ref RaycastHit hit)
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

    public abstract void Toggle(bool active);

}
