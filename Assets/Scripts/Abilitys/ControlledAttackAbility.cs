using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledAttackAbility : Ability, IPlayerAbilitys
{

	CharacterController _characterController;
	[SerializeField]
	float attackRange = 5;
	[SerializeField]
	float damage = 10;
	[SerializeField]
	float coolDown = 1;
	[SerializeField]
	LayerMask thingsToAttack;
	[SerializeField]
	float attackLentgh = 1;
	[SerializeField]
	GameObject hitParticle;
	[SerializeField]
	GameObject killParticle;

	RaycastHit hit;

	public override void OnStart()
	{
		_characterController = GetComponent<CharacterController>();
		_characterController.callEveryFrame += EveryFrame;
	}

	public override void EveryFrame()
	{
		if (AbilityPermitted)
		{
			Debug.DrawRay(transform.position+ (Vector3.up/2), transform.forward , Color.red, attackRange, false);
			if (InputManager.Instance.attackButton)
			{
				BeforeAbility();
			}
		}
	}

	public override void BeforeAbility()
	{
		Ray ray = new Ray(transform.position+ (Vector3.up/2), transform.forward);
		if (!_characterController.stateLocked)
		{
			_characterController.currentPlayerState = CharacterController.PlayerStates.attacking;
			_characterController.stateLocked = true;
		}
		if (Physics.Raycast(ray, out hit, attackRange, thingsToAttack))
		{
			WhileAbility();
		}
		else
		{
			Invoke("AfterAbility", attackLentgh);
		}
	}

	public override void WhileAbility()
	{
		CharacterController objectToHit = hit.transform.GetComponent<CharacterController>();
		objectToHit.health -= damage;
		if (objectToHit.health <= 0)
		{
			objectToHit.dead = true;

                GameController.Instance.score++;
            Instantiate(killParticle, hit.transform.position, Quaternion.identity);
		}
		else
		{
			Instantiate(hitParticle, hit.transform.position,Quaternion.identity);
		}
		Invoke("AfterAbility", attackLentgh);
	}

	public override void AfterAbility()
	{
		_characterController.stateLocked = false;
		_characterController.currentPlayerState = CharacterController.PlayerStates.idle;
	}
}
