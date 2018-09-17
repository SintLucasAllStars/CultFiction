using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{

	private int _rounds;
	
	public delegate void StartRound();
	public static event StartRound OnStartRound;

	public delegate void EndRound();
	public static event EndRound OnEndRound;

	private bool _isPlaying;

	public static CourseManager Instance;
	

	private void Awake()
	{
		Instance = this;
	}

	void Start ()
	{
		if(OnStartRound != null)
		OnStartRound();
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
	}

	public void StartNewRound()
	{
		_isPlaying = false;
		_rounds++;
		OnStartRound();
	}

	public void StopRound()
	{
		OnEndRound();
	}

	public void RestartRound()
	{
		_rounds++;
		
	}

	public void EndGame()
	{
		GameManager.Instance.EndCourse(this, _rounds);
		//Unload all course objects
	}
}
