﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreText : MonoBehaviour {
	public Text scoreText;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Rabbits killed: " + GameController.Instance.score;
	}

	public void Restart(){
		Application.LoadLevel(Application.loadedLevel);
	}
}
