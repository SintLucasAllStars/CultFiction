using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : OnRoadBase
{
    public int collisionDamage = 1;
    Material mats;

    Player m_player;

    protected override void Start()
    {
        base.Start();

        m_player = FindObjectOfType<Player>();
        mats = GetComponentInChildren<MeshRenderer>().materials[3];
        mats.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    void Update()
    {
        OnRoadMovement();
    }

    protected override void OnCarHit()
    {
        m_player.UpdateHealth(collisionDamage);
    }
}
