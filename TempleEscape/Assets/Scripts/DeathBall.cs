using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBall : MonoBehaviour, ITriggerable
{

    private Vector3 _startPos;
    private Rigidbody _rb;
    private int _deathBallSpeed;

    private void Start()
    {
        _startPos = this.transform.position;
        _rb = this.GetComponent<Rigidbody>();
        _deathBallSpeed = 7;
    }

    public void Triggerd()
    {
        _rb.isKinematic = false;
        _rb.AddForce(new Vector3(_deathBallSpeed, 0, 0), ForceMode.Impulse);
    }

    public void Reset()
    {
        this.transform.position = _startPos;
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
    }
}
