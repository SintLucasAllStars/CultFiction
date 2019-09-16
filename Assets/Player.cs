using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float _speed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            _animator.SetInteger("Direction", 1);
            _rigidBody.velocity = new Vector2(0, _speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            _animator.SetInteger("Direction", -1);
            _rigidBody.velocity = new Vector2(0, -_speed);
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _animator.SetInteger("Direction", 3);
            _rigidBody.velocity = new Vector2(-_speed, 0);
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _animator.SetInteger("Direction", 2);
            _rigidBody.velocity = new Vector2(_speed, 0);
        }
        else
        {
            _animator.SetInteger("Direction", 0);
            _rigidBody.velocity = new Vector2(0, 0);
        }
    }
}