using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerMeter : MonoBehaviour
{

	private Image _fill;

	void Start ()
	{
		_fill = GetComponentsInChildren<Image>()[1];
	}
	
	void Update ()
	{
		_fill.fillAmount = PlayerController.Instance.Power;
	}
}
