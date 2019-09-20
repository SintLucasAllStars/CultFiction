using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour, ITriggerable
{
    private Vector3 _startPos;
    private Rigidbody _rb;

    private void Start()
    {
        _startPos = transform.position;
        _rb = this.GetComponent<Rigidbody>();
    }

    public void Triggerd()
    {
        _rb.isKinematic = false;
    }

    public void Reset()
    {
        _rb.isKinematic = true;
        transform.position = _startPos;
    }
}
