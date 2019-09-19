using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singelton<Player>
{
    public Camera playerCam;

    //Movement references
    private float _speed;
    private Rigidbody _rb;

    //Jumping references
    private bool _isJumping;
    private float _jumpVelocity;

    //Rotation references
    private readonly float _sensetivity = 1.5f;
    private float _xCameraRotation;

    private Vector3 _startPos; 

    private bool _paused;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _speed = 5f;
        _paused = false;
        _isJumping = false;
        _jumpVelocity = 5;
        _startPos = transform.position;
    }

    private void Update()
    {
        if (!_paused)
        {
            Rotate();
            Jump();
            Sprint();
            Crouch();
        }
    }

    private void FixedUpdate()
    {
        if (!_paused)
        {
            Move();

            if (_isJumping)
            {
                _rb.AddForce(new Vector3(0, _jumpVelocity, 0), ForceMode.Impulse);
                _isJumping = false;
            }
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movementHorizontal = transform.right * horizontal;
        Vector3 movementVertical = transform.forward * vertical;

        Vector3 velocity = (movementHorizontal + movementVertical).normalized * _speed;

        _rb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void Rotate()
    {
        float minX = -60;
        float maX = 80;

        float YPlayerRotation = Input.GetAxisRaw("Mouse X");
        _xCameraRotation += -Input.GetAxisRaw("Mouse Y") * _sensetivity;
        _xCameraRotation = Mathf.Clamp(_xCameraRotation, minX, maX);

        transform.Rotate(0, YPlayerRotation * _sensetivity, 0);
        playerCam.transform.localRotation = Quaternion.Euler(_xCameraRotation, 0, 0);
    }

    private void Jump()
    {
        RaycastHit hit;
        Debug.DrawRay(this.transform.position, transform.forward.normalized * 40, Color.red);

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, out hit, 2))
        _isJumping = true;
    }

    private void Sprint()
    {
        float multiplierSpeed = 1.5f;
   
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _speed *= multiplierSpeed;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            _speed /= multiplierSpeed;
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y / 2, this.transform.localScale.y);
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            this.transform.position += new Vector3(0, this.transform.localScale.y /2 , 0);
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y * 2, this.transform.localScale.z);
        }
    }

    public void OnDeath()
    {
        transform.position = _startPos;
        transform.localRotation = Quaternion.Euler(0, 90, 0);
    }
}