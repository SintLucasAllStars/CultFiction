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

    public override void Fire()
    {
        if(ammo > 0)
        {
            animator.Play("Fire");

            RaycastHit hit = new RaycastHit();
            if(RaycastForward(ref hit))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if(enemy != null)
                    enemy.Damage(10);
                Debug.Log(enemy);
            }
            MuzzleFlash();
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

    public override void Toggle(bool active)
    {
        gameObject.SetActive(active);
    }
}
