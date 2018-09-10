using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{

	private int _rounds;
	
	public delegate void StartRound();
	public static event StartRound OnStartRound;

	private Rigidbody _ballRb;
	
	void Start ()
	{
		if(OnStartRound != null)
		OnStartRound();
		_ballRb = GameObject.FindWithTag("Ball").GetComponent<Rigidbody>();
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
		OnStartRound();
	}

	public void EndGame()
	{
		GameManager.Instance.EndCourse(this, _rounds);
		//Unload all course objects
	}
}
