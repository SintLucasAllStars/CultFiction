using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{

	private bool _isMoving;
	private Rigidbody _rigidbody;
	private Vector3 _lastPostion;
	private TrailRenderer _trail;

	public bool OutOfBounds = false;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_trail = GetComponent<TrailRenderer>();
	}

	private void OnEnable()
	{
		CourseManager.OnStartRound += OnStartRound;
		PlayerController.onHitBall += OnHitBall;
	}

	private void OnDisable()
	{
		CourseManager.OnStartRound -= OnStartRound;
		PlayerController.onHitBall -= OnHitBall;
	}

	private void OnStartRound()
	{
		_lastPostion = transform.position;
		OutOfBounds = false;
		_trail.Clear();
	}

	private void OnHitBall()
	{
		_isMoving = true;
	}

	void Update () {
		
		if (_isMoving)
		{
			if (_rigidbody.IsSleeping())
			{
				CourseManager.Instance.StopRound();
				_isMoving = false;
			}
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		GameObject hitObject = other.gameObject;
		if (hitObject.CompareTag("EndPoint"))
		{
			CourseManager.Instance.StopGame();
			_isMoving = false;
		}
		else if (hitObject.CompareTag("Outside"))
		{
			_isMoving = false;
			OutOfBounds = true;
			_rigidbody.velocity = Vector3.zero;
			transform.position = _lastPostion;
			CourseManager.Instance.StopRound();
		}
	}
}
