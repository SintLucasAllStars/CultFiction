using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAbility : MonoBehaviour {
	[SerializeField]
	ParticleSystem runParticle;

	CharacterController characterController;
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (runParticle != null)
		{
			if (characterController.playerSpeed > 5)
			{
				runParticle.enableEmission = true;
			}
			else
			{
				runParticle.enableEmission = false;
			}
		}
	}
}
