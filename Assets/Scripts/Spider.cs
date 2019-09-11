using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField]
    protected int _followerSlots = 3;

    [SerializeField]
    protected float _turnSpeed;

    [SerializeField]
    protected GameObject _body;

    [SerializeField]
    private float _speed = 0.0f;

    protected List<FollowerSpider> _followers = new List<FollowerSpider>();

    protected Animator _animator;

    private bool _isWalking = false;

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public bool IsWalking
    {
        get { return _isWalking; }

        set
        {
            _isWalking = value;
            _animator.SetBool("isWalking", _isWalking);
        }
    }

    protected virtual void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
    }

    public virtual void  AddFollower(FollowerSpider follower)
    {
        _followers.Add(follower);
    }

    public virtual void RemoveFollower(FollowerSpider follower)
    {
        if (_followerSlots > 0)
        {
            _followers.Remove(follower);
        }
    }

    protected virtual void Walk()
    {
        transform.Translate(_body.transform.forward * Time.deltaTime * Speed);
    }

    protected virtual void Rotate(float xDir, float zDir)
    {
        Vector3 moveDir = Quaternion.LookRotation(new Vector3(xDir, 0, zDir), Vector3.up).eulerAngles;
        Vector3 bodyDir = _body.transform.rotation.eulerAngles;

        float newDir = Mathf.MoveTowardsAngle(bodyDir.y, moveDir.y - _body.transform.position.y, _turnSpeed * Time.deltaTime);
        _body.transform.rotation = Quaternion.Euler(0, newDir, 0);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }
}
