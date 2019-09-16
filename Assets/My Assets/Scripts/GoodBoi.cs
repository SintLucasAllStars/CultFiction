using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoi : Soldier
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Move();
    }

    public override void Move()
    {
        base.Move();
        Debug.Log("Goodboi subclass");
    }
}
