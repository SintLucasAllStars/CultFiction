using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{

	private Animation _poleAnimation;
	private bool _poleEnabled = true;
	[SerializeField] private float _playerPoleDistance;
	private CourseManager _course;
	
	void Start ()
	{
		_poleAnimation = transform.GetComponentInChildren<Animation>();
		_course = GetComponentInParent<CourseManager>();
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
		Debug.Log(Vector3.Distance(PlayerController.Instance.transform.position, transform.position) <= _playerPoleDistance);
		if (Vector3.Distance(PlayerController.Instance.transform.position, transform.position) <= _playerPoleDistance)
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
		if (other.collider.CompareTag("Ball"))
		{
			_course.EndGame();
		}
	}
}
