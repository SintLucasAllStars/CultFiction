using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager> {
	[HideInInspector]
	public float horizontalAxis;
	[HideInInspector]
	public float verticalAxis;
	[HideInInspector]
	public bool jumpButton;
    [HideInInspector]
	public bool interactButton;
    [HideInInspector]
    public bool runButton;   
    [HideInInspector]
	public bool slowWalkButton;
    [HideInInspector]
    public bool attackButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		verticalAxis = Input.GetAxis("Vertical");
		horizontalAxis = Input.GetAxis("Horizontal");
		jumpButton = Input.GetButtonDown ("Jump");
		interactButton = Input.GetButtonDown ("Interact");
		runButton = Input.GetButton("Run")&&Input.GetButton("Vertical");
		slowWalkButton = Input.GetButton("SlowWalk")&& Input.GetButton("Vertical");
		attackButton = Input.GetButtonDown("Fire1");
	}
}
