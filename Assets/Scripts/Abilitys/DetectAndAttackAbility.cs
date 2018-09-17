using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DetectAndAttackAbility : Ability, IPlayerAbilitys
{
	[SerializeField]
	float attackDistance = 5;
	[SerializeField]
	float damage = 100;
	[SerializeField]
	float attackRange = 5;
	[SerializeField]
	LayerMask thingsToAttack;

	RaycastHit hit;
	[SerializeField]
	GameObject hitParticle;
	[SerializeField]
	GameObject killParticle;

	CharacterController _characterController;
	CharacterController thingToAttack;
	bool seesPLayer = false;
	WayPointWalkAbility wayPointWalkAbility;
	NavMeshAgent navAgent;
	bool attacking;
	public override void OnStart()
	{
		_characterController = GetComponent<CharacterController>();
		wayPointWalkAbility = GetComponent<WayPointWalkAbility>();
		_characterController.callEveryFrame += EveryFrame;
		navAgent = GetComponent<NavMeshAgent>();
		BeforeAbility();
	}

	public override void EveryFrame()
	{
		if (AbilityPermitted && thingToAttack != null && !attacking)
		{
			if (Vector3.Distance(thingToAttack.transform.position, transform.position) <= attackDistance)
			{
				BeforeAbility();
			}
		}
		else
		{
			if (wayPointWalkAbility.AbilityPermitted == false)
			{
				wayPointWalkAbility.AbilityPermitted = true;
			}
		}
		if (AbilityPermitted && attacking)
		{
			WhileAbility();
		}
	}
	public override void BeforeAbility()
	{
		wayPointWalkAbility.AbilityPermitted = false;
		attacking = true;
	}

	public override void WhileAbility()
	{
		
		if (thingToAttack != null)
		{
			Ray ray = new Ray(transform.position + (Vector3.up / 2), thingToAttack.transform.position - transform.position);

			Debug.DrawRay(transform.position + (Vector3.up / 2), thingToAttack.transform.position - transform.position, Color.blue,attackRange , false);
			if (Physics.Raycast(ray, out hit, attackRange, thingsToAttack))
			{
                navAgent.destination = thingToAttack.transform.position;
                navAgent.speed = wayPointWalkAbility.runSpeed;
                if (Vector3.Distance(thingToAttack.transform.position, transform.position) <= 1f)
                {
                    PerformAttack();
                }
			}
		
		}
		else
		{
			AfterAbility();
		}
	}
	void PerformAttack()
	{
        _characterController.currentPlayerState = CharacterController.PlayerStates.attacking;
		navAgent.speed = 0;
		_characterController.stateLocked = true;
		thingToAttack.health -= damage;
		if (thingToAttack.health <= 0)
		{
			thingToAttack.dead = true;
			Instantiate(killParticle, thingToAttack.transform.position, Quaternion.identity);
			AfterAbility();
		}
		else
		{
			Instantiate(hitParticle, thingToAttack.transform.position, Quaternion.identity);
		}
	}
	public override void AfterAbility()
	{
		thingToAttack = null;
		_characterController.stateLocked = false;
		navAgent.speed = wayPointWalkAbility.speed;
	}
	void OnTriggerEnter(Collider col)
	{
		if (AbilityPermitted)
		{
			if (col.CompareTag("Player") || col.CompareTag("Bunny") && !seesPLayer)
			{
				thingToAttack = col.GetComponent<CharacterController>();
				if (col.CompareTag("Player"))
				{
					seesPLayer = true;
				}
			}
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (AbilityPermitted)
		{
			if (col.CompareTag("Player") || col.CompareTag("Bunny"))
			{
				if (thingToAttack == col.GetComponent<CharacterController>())
				{
					thingToAttack = null;
					attacking = false;
					AfterAbility();
				}
				if (col.CompareTag("Player"))
				{
					seesPLayer = false;
				}
			}
		}
	}
}
