using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{ 
    [SerializeField] Transform target;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float rotationalDamp = 0.5f;

    private void Start()
    {
        target = GameObject.FindWithTag ("Player").transform;
    }

    void Update()
    {
        Turn();
        Move();
    }
    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp *  Time.deltaTime);
    }
    void Move()
    {
        transform.position += transform.forward * movementSpeed* Time.deltaTime;

    }

   
}
