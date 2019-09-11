using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField]
    protected float _turnSpeed;

    [SerializeField]
    protected GameObject _body;

    [SerializeField]
    private float _speed = 0.0f;

    protected bool _isWalking = false;

    protected float Speed { get => _speed; set => _speed = value; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected virtual void Walk()
    {
        transform.Translate(_body.transform.forward * Time.deltaTime * Speed);
    }
}
