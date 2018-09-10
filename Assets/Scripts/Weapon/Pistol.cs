using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pistol : Weapon
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
        if(ammo <= 0)
            animator.SetBool("isEmpty", true);
        PlayAudioSource(fire);
    }

    public override void Reload()
    {
        if(ammo == maxAmmo || ammoStockPile == 0)
            return;

        base.Reload();
        animator.SetBool("isEmpty", false);
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

    public override void Toggle(bool active)
    {
        base.Toggle(active);
        if(animator == null)
            animator = GetComponent<Animator>();
        animator.Play("Wield");
    }
}
