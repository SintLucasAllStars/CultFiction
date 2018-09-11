using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{

	private int _rounds;
	
	public delegate void StartRound();
	public static event StartRound OnStartRound;

	private Rigidbody _ballRb;

	private bool _isPlaying;
	
	void Start ()
	{
		if(OnStartRound != null)
		OnStartRound();
		_ballRb = GameObject.FindWithTag("Ball").GetComponent<Rigidbody>();
	}

	private void OnEnable()
	{
		PlayerController.onHitBall += OnHitBall;
	}

	private void OnDisable()
	{
		PlayerController.onHitBall -= OnHitBall;
	}

	private void OnHitBall()
	{
		_isPlaying = true;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			OnStartRound();
		}

		if (_isPlaying)
		{
			if (_ballRb.IsSleeping())
			{
				StartNewRound();
			}
		}
	}

	public void StartNewRound()
	{
		_isPlaying = false;
		OnStartRound();
	}

	public void EndGame()
	{
		GameManager.Instance.EndCourse(this, _rounds);
		//Unload all course objects
	}
}
