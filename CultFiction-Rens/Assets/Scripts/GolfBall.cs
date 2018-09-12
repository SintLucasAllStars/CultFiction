using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{

	private bool _isMoving;
	private Rigidbody _rigidbody;
	private Vector3 _lastPostion;
	
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
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
				CourseManager.Instance.StartNewRound();
				_isMoving = false;
			}
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		GameObject hitObject = other.gameObject;
		if (hitObject.CompareTag("EndPoint"))
		{
			CourseManager.Instance.EndGame();
		}
		else if (hitObject.CompareTag("Outside"))
		{
			_rigidbody.velocity = Vector3.zero;
			transform.position = _lastPostion;
			CourseManager.Instance.StartNewRound();
		}
	}
}
