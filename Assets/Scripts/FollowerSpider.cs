using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerSpider : Spider
{
    [SerializeField]
    private float _preferedDistance = 0.1f;

    private Spider leader;

    public Spider Leader { get => leader; set => leader = value; }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Leader != null)
        {
            float distanceFromLeader = Vector3.Distance(Leader.gameObject.transform.position, transform.position);
            IsWalking = distanceFromLeader > _preferedDistance;
            if (IsWalking)
            {
                Walk();
            }
        }
        else
        {

        }
    }

    protected override void Walk()
    {
        base.Walk();
        Rotate(Leader.transform.position.x - transform.position.x, Leader.transform.position.z - transform.position.z);
    }

    protected override void Rotate(float xDir, float zDir)
    {
        base.Rotate(xDir, zDir);
    }
}
