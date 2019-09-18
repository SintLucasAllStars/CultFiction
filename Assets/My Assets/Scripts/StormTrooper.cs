using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class StormTrooper : Soldier
{
    public override void Start()
    {
        base.Start();
        Move();
    }

    public override void Move()
    {
        base.Move();
        Debug.Log("stormtrooper subclass ");
    }

    public override void Select()
    {
        base.Select();
    }

    public override void Shoot(GameObject target)
    {
        base.Shoot(target);
    }
    
}
