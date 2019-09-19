using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBall : MonoBehaviour, ITriggerable
{

    private Rigidbody _rb;
    private int _deathBallSpeed;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _deathBallSpeed = 7;
    }

    public void Triggerd()
    {
        _rb.AddForce(new Vector3(_deathBallSpeed, 0, 0), ForceMode.Impulse);
    }

    public void Reset()
    {
        
    }
}
