using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationAbility : Ability, IPlayerAbilitys
{
	[SerializeField]
	Animator animator;
	CharacterController _characterController;
	CharacterController.PlayerStates oldState;

	float lerp;
	bool lerpUp = true;

	public override void OnStart()
	{
		_characterController = GetComponent<CharacterController>();

        _characterController.callEveryFrame += EveryFrame;
		_characterController.characterDied += Died;
		BeforeAbility();
	}

	public override void EveryFrame()
	{
		if (AbilityPermitted)
		{

			WhileAbility();
		}
	}
	void Died(){
		animator.speed = 0;
		animator.enabled = false;
	}
	public override void BeforeAbility()
	{
		StartCoroutine(SetRandomIdle());
	}

	public override void WhileAbility()
	{
		if (_characterController.currentPlayerState == CharacterController.PlayerStates.moving)
        {
            animator.SetFloat("Speed", _characterController.playerSpeed);
        }
        if (_characterController.grounded)
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
		if (oldState != _characterController.currentPlayerState)
		{
			switch (_characterController.currentPlayerState)
			{
				case CharacterController.PlayerStates.idle:
					animator.SetBool("Idle", true);
					animator.SetBool("Moving", false);
					animator.SetBool("Jump", false);
					animator.SetBool("Die", false);
					break;
				case CharacterController.PlayerStates.moving:
					animator.SetBool("Moving", true);
					animator.SetBool("Idle", false);
					animator.SetBool("Jump", false);
					animator.SetBool("Die", false);
					break;
				case CharacterController.PlayerStates.jump:
					animator.SetBool("Jump", true);
					animator.SetBool("Moving", false);
					animator.SetBool("Idle", false);
					animator.SetBool("Die", false);
					break;
				case CharacterController.PlayerStates.die:
					animator.SetBool("Die", true);
					animator.SetBool("Idle", false);
					animator.SetBool("Moving", false);
					animator.SetBool("Jump", false);
					break;
				case CharacterController.PlayerStates.attacking:
					animator.SetTrigger("Attack");
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
					break;

			}
			oldState = _characterController.currentPlayerState;
		}
	}

	public override void AfterAbility()
	{

	}
	IEnumerator SetRandomIdle(){
		while (true)
		{

			yield return new WaitForSeconds(0.5f);
			if (lerp > 1)
            {
				lerpUp = false;
            }
			if (lerp < 0)
            {
				lerpUp = true;
            }
			if(lerpUp){
				lerp += 0.005f;
			}else{
				lerp -= 0.005f;
			}
			animator.SetFloat("RandomIdle",Mathf.Lerp(0,1,lerp));
		}
	}
}
