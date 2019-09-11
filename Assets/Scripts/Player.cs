using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Spider
{
    private float _xDir;
    private float _zDir;

    void Start()
    {
        
    }

    void Update()
    {
        _xDir = Input.GetAxisRaw("Horizontal");
        _zDir = Input.GetAxisRaw("Vertical");

        if (_xDir != 0 || _zDir != 0)
        {
            Walk();
        }
    }

    protected override void Walk()
    {
        base.Walk();
        Rotate();
    }

    private void Rotate()
    {
        Vector3 moveDir = Quaternion.LookRotation(new Vector3(_xDir, 0, _zDir), Vector3.up).eulerAngles;
        Vector3 bodyDir = _body.transform.rotation.eulerAngles;

        float newDir = Mathf.MoveTowardsAngle(bodyDir.y, moveDir.y - _body.transform.position.y, _turnSpeed * Time.deltaTime);
        //Vector3 rotateDir = Vector3.RotateTowards(bodyDir, moveDir - _body.transform.position, _turnSpeed * Time.deltaTime, _turnSpeed);
        //Vector3 newDir = Vector3.RotateTowards(bodyDir, moveDir - _body.transform.position, _turnSpeed * Time.deltaTime, _turnSpeed);
        _body.transform.rotation = Quaternion.Euler(0, newDir, 0);
    }
}
