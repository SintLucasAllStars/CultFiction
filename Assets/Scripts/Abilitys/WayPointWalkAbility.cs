using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class WayPointWalkAbility : Ability, IPlayerAbilitys {
    private CharacterController _playerController;
    [SerializeField]
    float speed = 1;
	[SerializeField]
	Transform[] wayPoints;
	[SerializeField]
	bool randomStopMoment;
	Transform currentWaypoint;
	NavMeshAgent navMeshAgent;
	float randomTime;

	bool reached = false;
	int wayPointIndex;
    public override void OnStart()
    {
        _playerController = GetComponent<CharacterController>();
		navMeshAgent = GetComponent<NavMeshAgent>();

    }

    public override void EveryFrame()
    {
        if (AbilityPermitted)
        {
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
        navMeshAgent.speed = speed;
		randomTime = Random.Range(4.0f, 15.0f);
		StartCoroutine(Timer(randomTime));
    }

    public override void WhileAbility()
    {      
		float dist= navMeshAgent.remainingDistance; 
		if (dist!=Mathf.Infinity && navMeshAgent.pathStatus==NavMeshPathStatus.PathComplete && navMeshAgent.remainingDistance<=1){
			AfterAbility();
		}
    }

    public override void AfterAbility()
    {
		reached = true;      
    }
	IEnumerator Timer(float time){
		yield return new WaitForSeconds(time);
		AfterAbility();
	}
}

