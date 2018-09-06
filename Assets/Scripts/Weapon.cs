using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] Transform muzzle;

    [SerializeField] protected int ammo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int ammoStockPile;

    [SerializeField] protected float fireDistance = 1000;
    

    public abstract void Fire();

    protected bool RaycastForward(ref RaycastHit hit)
    {
        if(Physics.Raycast(muzzle.transform.position, transform.forward, out hit, fireDistance))
        {
            Debug.DrawRay(muzzle.transform.position, transform.forward, Color.red, 20);
            return true;
        }
        return false;
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
