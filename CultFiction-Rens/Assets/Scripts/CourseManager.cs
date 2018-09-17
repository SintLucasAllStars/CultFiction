using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{

	public int Rounds { get; private set; }
	
	public delegate void StartRound();
	public static event StartRound OnStartRound;

	public delegate void EndRound();
	public static event EndRound OnEndRound;

	public delegate void EndGame();
	public static event EndGame OnEndGame;

	private bool _isPlaying;
	private bool _isEnded;

	public static CourseManager Instance;
	

	private void Awake()
	{
		Instance = this;
	}

	void Start ()
	{
		if(OnStartRound != null)
		OnStartRound();

		_isEnded = false;
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
		Rounds++;
		OnStartRound();
	}

	public void StopRound()
	{
		if(_isEnded) return;
		
		OnEndRound();
	}

	public void RestartRound()
	{
		Rounds++;
		
	}

	public void StopGame()
	{
		_isEnded = true;
		OnEndGame();
	}
}
