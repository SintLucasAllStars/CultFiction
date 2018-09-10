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

    protected override void FireExtra()
    {
        animator.Play("Fire");
        if(ammo <= 0)
            animator.SetBool("isEmpty", true);
        PlayAudioSource(fire);
    }

    protected override void ReloadAnimation()
    {
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
}
