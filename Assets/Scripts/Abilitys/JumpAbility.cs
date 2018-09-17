using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : Ability , IPlayerAbilitys {

	private CharacterController _characterController;
	Rigidbody rb;
	public float jumpForce;

	public override void OnStart(){
		_characterController = GetComponent<CharacterController> ();
		_characterController.callEveryFrame += EveryFrame;
		rb = GetComponent<Rigidbody> ();
	}

	public override void EveryFrame(){
		if (AbilityPermitted)
		{
			if (InputManager.Instance.jumpButton && _characterController.grounded)
			{
				BeforeAbility ();
			}
		}
	}

	public override void BeforeAbility(){
		
		if (!_characterController.stateLocked)
		{
			_characterController.currentPlayerState = CharacterController.PlayerStates.jump;
			_characterController.stateLocked = true;
		}
		WhileAbility ();      
	}

	public override void WhileAbility(){
		rb.AddForce (new Vector3 (0, jumpForce, 0));
		Invoke("AfterAbility",1f);
	}

	public override void AfterAbility(){
		_characterController.stateLocked = false;
	}
}
