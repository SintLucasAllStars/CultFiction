using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class m1911 : Weapon
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Fire()
    {
        animator.Play("Fire");
        RaycastHit hit = new RaycastHit();
        RaycastForward(ref hit);
    }

    protected override void ReloadAnimation()
    {
        animator.Play("Reload");
    }

    public override void Toggle(bool active)
    {
        gameObject.SetActive(active);
    }
}
