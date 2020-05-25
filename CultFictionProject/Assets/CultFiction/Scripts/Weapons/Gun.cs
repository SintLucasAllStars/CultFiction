using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class Gun : MonoBehaviour, IInteraction
{




    public int ammoCount;
    public VisualEffect Muzzle;
    public bool canGrab;

    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject MuzzleLoc;
    [SerializeField]
    private float GunForceStrength;
    [SerializeField]
    private int GunDamage;
    

    private PlayerController currentPC;



    public void Fire()
    {
        var go = Instantiate(Bullet, MuzzleLoc.transform.position, Quaternion.identity);
        go.GetComponent<Bullet>().damage = GunDamage;
        go.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 2500);
        Muzzle.Play();
        ammoCount--;
        if(ammoCount <= 0)
        {
            DestroyGun();
        }
    }

    void DestroyGun()
    {
        if (currentPC)
        {
            currentPC.SetArmPos(ArmPoses.None);
            currentPC.currentGun = null;
        }
        Destroy(this.gameObject);
    }

    public void Grab(PlayerController pc)
    {
        if (canGrab)
        {
            if (pc.currentGun && pc.currentGun != this)
            {
                pc.currentGun.DestroyGun();
            }

            this.transform.parent = pc.GunLoc;
            this.transform.localPosition = Vector3.zero;
            this.transform.localEulerAngles = Vector3.zero;
            this.transform.localScale = new Vector3(2, 2, 2);
            pc.currentGun = this;
            currentPC = pc;
        }

    }

    public void Use()
    {
        Fire();
    }

    public void Drop()
    {
        
        DestroyGun();
    }
}
