using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{

	private Animation _poleAnimation;
	private bool _poleEnabled = true;
	[SerializeField] private float _playerPoleDistance;
	private CourseManager _course;
	private GameObject _ball;
	
	void Start ()
	{
		_poleAnimation = transform.GetComponentInChildren<Animation>();
		_course = GetComponentInParent<CourseManager>();
		_ball = GameObject.FindGameObjectWithTag("Ball");
	}

	private void OnEnable()
	{
		CourseManager.OnStartRound += OnStartRound;
	}

	private void OnDisable()
	{
		CourseManager.OnStartRound -= OnStartRound;
	}

	private void OnStartRound()
	{
		if (Vector3.Distance(_ball.transform.position, transform.position) <= _playerPoleDistance)
		{
			if (!_poleEnabled) return;
			_poleAnimation.PlayQueued("RemovePole");
			_poleEnabled = false;
		}
		else
		{
			if(_poleEnabled) return;
			_poleAnimation.PlayQueued("PlacePole");
			_poleEnabled = true;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject == _ball)
		{
			_course.EndGame();
		}
	}
}
