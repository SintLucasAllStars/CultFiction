using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollowCamera : MonoBehaviour
{

	[SerializeField] private Transform[] _cameraPoints;
	private Transform _currentPoint;
	private GameObject _camera;
	private Transform _ball;
	
	void Start ()
	{
		_camera = GetComponentInChildren<Camera>().gameObject;
		_camera.SetActive(false);
		_ball = GameObject.FindGameObjectWithTag("Ball").transform;
	}

	private void OnEnable()
	{
		PlayerController.onHitBall += OnHitBall;
		CourseManager.OnStartRound += OnStartNewRound;
	}
	
	private void OnDisable()
	{
		PlayerController.onHitBall -= OnHitBall;
		CourseManager.OnStartRound -= OnStartNewRound;
	}

	private void OnStartNewRound()
	{
		if(_camera !=  null)
		_camera.SetActive(false);
	}

	private void OnHitBall()
	{
		_camera.SetActive(true);
		float shortestDist = 1000000000;
		foreach (Transform cameraPoint in _cameraPoints)
		{
			if (Vector3.Distance(cameraPoint.position, _ball.position) < shortestDist)
			{
				shortestDist = Vector3.Distance(cameraPoint.position, _ball.position);
				_currentPoint = cameraPoint;
			}
		}

		_camera.transform.position = _currentPoint.position;
		
	}

	void Update () {
		_camera.transform.LookAt(_ball.position);
	}
}
