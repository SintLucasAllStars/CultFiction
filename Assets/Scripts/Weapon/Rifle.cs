using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Rifle : Weapon
{
    Animator animator;
    [SerializeField] AudioSource fire;
    [SerializeField] AudioSource reload;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Fire()
    {
        if(!canFire)
            return;

        base.Fire();
        animator.Play("Fire");
        PlayAudioSource(fire);
    }

    public override void Reload()
    {
        if(ammo == maxAmmo || ammoStockPile == 0)
            return;

        base.Reload();
        if(ammo > 0)
        {
            animator.Play("Reload");
        }
        else if(ammoStockPile >= maxAmmo || ammoStockPile < maxAmmo)
        {
            animator.Play("ReloadEmpty");
        }
        PlayAudioSource(reload);
    }
}
