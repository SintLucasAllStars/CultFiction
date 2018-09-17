using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	private Text _text;

	public int ParScore;
	
	private void OnEnable()
	{
		if(_text == null)
		_text = GetComponent<Text>();

		string score;

		switch (CourseManager.Instance.Rounds)
		{
			case (4):
				score = "Albatross";
				break;
			case (5):
				score = "Eagle";
				break;
			case (6):
				score = "Birdy";
				break;
			case (7):
				score = "Par";
				break;
			case (8):
				score = "Bogey";
				break;
			case (9):
				score = "Double Bogey";
				break;
			case (10):
				score = "Triple Bogey";
				break;
			default:
				score = CourseManager.Instance.Rounds.ToString();
				break;
		}

		_text.text = score;
	}
}
