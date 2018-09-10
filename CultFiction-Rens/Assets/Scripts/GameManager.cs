using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField]
	private CourseManager[] _courses;
	private Dictionary<CourseManager, int> _scores;

	private CourseManager currentCourse;
	
	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(this);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(this);		
	}

	private void Start()
	{
		foreach (CourseManager course in _courses)
		{
			_scores.Add(course, 0);
		}
	}

	public void EndCourse(CourseManager course,int rounds)
	{
		_scores[course] = rounds;
	}
}
