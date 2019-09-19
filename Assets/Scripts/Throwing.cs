using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    [Range(10,100)]
    public float _throwingAngle;

    public float _rotationSpeed;

    [Range(50, 400)]
    public float _power;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private GameObject _arrow;

    [SerializeField]
    private float _angle;

    bool _isThrown;

    void Start()
    {
        _rotationSpeed *= _throwingAngle;
    }

    private void Update()
    {
        if (_isThrown)
        {
            return;
        }

        transform.Rotate(Vector3.up * _rotationSpeed);
        _angle += _rotationSpeed;
        if (_angle < -_throwingAngle / 2 || _angle > _throwingAngle / 2)
        {
            _rotationSpeed = -_rotationSpeed;
        }
    }

    public void Throw()
    {
        _isThrown = true;
        _arrow.SetActive(false);
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.forward * _power);
    }
}
