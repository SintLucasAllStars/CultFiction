using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerSpider : Spider
{
    [SerializeField]
    private float _preferedDistance = 0.1f;

    private Spider _leader;

    public Spider Leader
    {
        get { return _leader; }
        set { _leader = value; }
    }

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

    public override void Die()
    {
        base.Die();
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

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (Leader != null)
        {
            AddFollowerSpider(other);
        }        
    }

    protected override void AddFollowerSpider(Collider hitTrigger)
    {
        base.AddFollowerSpider(hitTrigger);
    }
}
