using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallInfo : MonoBehaviour
{

	private GolfBall _ball;

	private Text _text;
	
	void Start ()
	{
		_ball = FindObjectOfType<GolfBall>();
		_text = GetComponent<Text>();
	}

	private void OnEnable()
	{
		if(_ball == null)
			_ball = FindObjectOfType<GolfBall>();

		if (_text == null)
			_text = GetComponent<Text>();
		
		if (_ball.OutOfBounds)
		{
			_text.text = "OUT OF BOUNDS";
			return;
		}

		_text.text = Mathf.RoundToInt(Vector3.Distance(PlayerController.Instance.transform.position, _ball.transform.position)) + "m";
	}
}
