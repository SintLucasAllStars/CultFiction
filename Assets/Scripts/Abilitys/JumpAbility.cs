using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : Ability , IPlayerAbilitys {

	private CharacterController _playerController;
	Rigidbody rb;
	public float jumpForce;

	public override void OnStart(){
		_playerController = GetComponent<CharacterController> ();
		rb = GetComponent<Rigidbody> ();
	}

	public override void EveryFrame(){
		if (AbilityPermitted)
		{
			if (InputManager.Instance.jumpButton && _playerController.grounded)
			{
				BeforeAbility ();
			}
		}
	}

	public override void BeforeAbility(){
		
		if (!_playerController.stateLocked)
		{
			_playerController.currentPlayerState = CharacterController.PlayerStates.jump;
			_playerController.stateLocked = true;
		}
		WhileAbility ();      
	}

	public override void WhileAbility(){
		rb.AddForce (new Vector3 (0, jumpForce, 0));
		Invoke("AfterAbility",1f);
	}

	public override void AfterAbility(){
		_playerController.stateLocked = false;
	}
}
