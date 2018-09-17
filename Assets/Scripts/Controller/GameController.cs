using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
	public CharacterController player;
	public int score;
	public GameObject playerDiedWindow;
	// Use this for initialization
	void Start () {
		playerDiedWindow.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(player.dead){
			playerDiedWindow.SetActive(true);
		}
	}
}
