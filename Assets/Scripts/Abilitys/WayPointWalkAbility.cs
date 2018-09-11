using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class WayPointWalkAbility : Ability, IPlayerAbilitys {
    private CharacterController _characterController;
	public float speed = 1;
	public float  runSpeed= 5;
	[SerializeField]
	Transform[] wayPoints;
	[SerializeField]
	bool randomStopMoment;
	public float finalSpeed;
	Transform currentWaypoint;
	NavMeshAgent navMeshAgent;
	float randomTime;

	bool reached = false;
	int wayPointIndex;
	public override void OnStart()
    {
        _characterController = GetComponent<CharacterController>();

        _characterController.callEveryFrame += EveryFrame;
		_characterController.characterDied += Died;
		navMeshAgent = GetComponent<NavMeshAgent>();
		finalSpeed = speed;
    }
	void Died(){
		navMeshAgent.Stop();
	}
    public override void EveryFrame()
    {
        if (AbilityPermitted)
		{
            _characterController.playerSpeed = finalSpeed;

			if (navMeshAgent.speed > 0)
            {
                _characterController.currentPlayerState = CharacterController.PlayerStates.moving;
            }
			if (!navMeshAgent.hasPath || reached)
			{
				BeforeAbility();
			}
			else
			{
				WhileAbility();
			}
		}
		else
		{
			navMeshAgent.enabled = false;
		}
    }

    public override void BeforeAbility()
    {
		wayPointIndex = Random.Range(0, wayPoints.Length);
		reached = false;
		navMeshAgent.SetDestination(wayPoints[wayPointIndex].position);
		navMeshAgent.speed = finalSpeed;
		randomTime = Random.Range(4.0f, 15.0f);
		StartCoroutine(Timer(randomTime));

    }

    public override void WhileAbility()
    {      
		navMeshAgent.speed = finalSpeed;
		float dist= navMeshAgent.remainingDistance; 
		if (dist!=Mathf.Infinity && navMeshAgent.pathStatus==NavMeshPathStatus.PathComplete && navMeshAgent.remainingDistance<=2){
			AfterAbility();
		}
    }

    public override void AfterAbility()
    {
		reached = true;      
    }
	IEnumerator Timer(float time){
		yield return new WaitForSeconds(time);
		if (Random.Range(0, 2) == 1&&randomStopMoment&&finalSpeed != runSpeed)
		{
			navMeshAgent.speed = 0;
			_characterController.currentPlayerState = CharacterController.PlayerStates.idle;
			StartCoroutine(IdleTime(Random.Range(5, 10)));
		}
		else
		{
			AfterAbility();
		}
	}
	IEnumerator IdleTime(float time){
        yield return new WaitForSeconds(time);
		navMeshAgent.speed = speed;
		AfterAbility();
	}
}

