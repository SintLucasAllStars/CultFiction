using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Spider
{
    private float _xDir;
    private float _zDir;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        _xDir = Input.GetAxisRaw("Horizontal");
        _zDir = Input.GetAxisRaw("Vertical");

        IsWalking = _xDir != 0 || _zDir != 0;
        if (IsWalking)
        {
            Walk();
        }
    }

    public override void AddFollower(FollowerSpider follower)
    {
        base.AddFollower(follower);
    }

    public override void RemoveFollower(FollowerSpider follower)
    {
        base.RemoveFollower(follower);
    }

    protected override void Walk()
    {
        base.Walk();
        Rotate(_xDir, _zDir);
    }

    protected override void Rotate(float xDir, float zDir)
    {
        base.Rotate(xDir, zDir);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        FollowerSpider hitSpider = other.gameObject.GetComponent<FollowerSpider>();
        if (hitSpider != null && _followerSlots < _followers.Count)
        {
            AddFollower(hitSpider);
            hitSpider.Leader = this;
        }
    }
}
