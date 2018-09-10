using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
	[SerializeField]
	Animator animator;
	CharacterController _playerController;
	CharacterController.PlayerStates oldState;
	// Use this for initialization
	void Start () {
		_playerController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_playerController.currentPlayerState == CharacterController.PlayerStates.moving)
		{
			animator.SetFloat("Speed", _playerController.playerSpeed);
		}
		if (oldState != _playerController.currentPlayerState)
		{
			switch (_playerController.currentPlayerState)
			{
				case CharacterController.PlayerStates.idle:
					animator.SetBool("Idle", true);
					animator.SetBool("Moving", false);
					animator.SetBool("Jump", false);
					animator.SetBool("Die", false);
					animator.SetBool("Attack", false);
					break;
				case CharacterController.PlayerStates.moving:
					animator.SetBool("Moving", true);
					animator.SetBool("Idle", false);
					animator.SetBool("Jump", false);
					animator.SetBool("Die", false);
					animator.SetBool("Attack", false);
					break;
				case CharacterController.PlayerStates.jump:
					animator.SetBool("Jump", true);
                    animator.SetBool("Moving", false);
					animator.SetBool("Idle", false);
					animator.SetBool("Die", false);
					animator.SetBool("Attack", false);
					break;
				case CharacterController.PlayerStates.die:
					animator.SetBool("Die", true);
					animator.SetBool("Idle", false);
					animator.SetBool("Moving", false);
					animator.SetBool("Jump", false);
					animator.SetBool("Attack", false);
					break;
				case CharacterController.PlayerStates.attack:
					animator.SetBool("Attack", true);
					animator.SetBool("Idle", false);
					animator.SetBool("Moving", false);
					animator.SetBool("Jump", false);
					animator.SetBool("Die", false);
					break;
				default:
					animator.SetBool("Idle", true);
					animator.SetBool("Moving", false);
					animator.SetBool("Jump", false);
					animator.SetBool("Die", false);
					animator.SetBool("Attack", false);
					break;

			}
			oldState = _playerController.currentPlayerState;
		}
	}
}
