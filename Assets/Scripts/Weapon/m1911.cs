using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class m1911 : Weapon
{
    Animator animator;

    [SerializeField] AudioClip[] gunSounds;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void FireExtra()
    {
        if(ammo > 0)
        {
            animator.Play("Fire");
            PlayGunAudio(gunSounds[0]);
        }
        else
        {
            animator.Play("FireEmpty");
        }
    }

    protected override void ReloadAnimation()
    {
        animator.Play("Reload");
        PlayGunAudio(gunSounds[1]);
    }
}
