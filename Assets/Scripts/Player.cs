using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private Animator _animator = null;

    private Rigidbody2D _rigidBody = null;

    [SerializeField]
    private float _speed = 0f;

    private OrderProcessor _orderProcessor;
    public OrderProcessor OrderProcessor => _orderProcessor;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _orderProcessor = GetComponentInChildren<OrderProcessor>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            _animator.SetInteger("Direction", 1);
            transform.localScale = new Vector3(1, 1, 0);
            _rigidBody.velocity = new Vector2(0, _speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            _animator.SetInteger("Direction", -1);
            transform.localScale = new Vector3(1, 1, 0);
            _rigidBody.velocity = new Vector2(0, -_speed);
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _animator.SetInteger("Direction", 3);
            transform.localScale = new Vector3(1, 1, 0);
            _rigidBody.velocity = new Vector2(-_speed, 0);
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _animator.SetInteger("Direction", 3);
            transform.localScale = new Vector3(-1, 1, 0);
            _rigidBody.velocity = new Vector2(_speed, 0);
        }
        else
        {
            _animator.SetInteger("Direction", 0);
            transform.localScale = new Vector3(1, 1, 0);
            _rigidBody.velocity = new Vector2(0, 0);
        }
    }
}