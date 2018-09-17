using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class WayPointWalkAbility : Ability, IPlayerAbilitys
{
	private CharacterController _characterController;
	public float speed = 1;
	public float runSpeed = 5;
	[SerializeField]
	public List<Transform> wayPoints;
	[SerializeField]
	bool randomStopMoment;
	public float finalSpeed;
	public Transform currentWaypoint;
	NavMeshAgent navMeshAgent;
	float randomTime;
	private NavMeshHit hit;
	public Vector3[] blockedPos;
	public NavMeshPath path;
	bool reached = false;
	int wayPointIndex;
	[SerializeField]
	bool debugPath;
	public override void OnStart()
	{
		_characterController = GetComponent<CharacterController>();

		_characterController.callEveryFrame += EveryFrame;
		_characterController.characterDied += Died;
		navMeshAgent = GetComponent<NavMeshAgent>();
		finalSpeed = speed;
		path = new NavMeshPath();
	}
	void Died()
	{
		navMeshAgent.Stop();
	}
	public override void EveryFrame()
	{
		if (AbilityPermitted)
		{
			if (debugPath)
			{
				for (int i = 0; i < path.corners.Length - 1; i++)
					Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
			}
			_characterController.playerSpeed = finalSpeed;
			if (navMeshAgent.speed > 0 && navMeshAgent.hasPath)
			{
				_characterController.currentPlayerState = CharacterController.PlayerStates.moving;
			}
			if (blockedPos.Length<1)
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
			//if (GetComponent<DetectAndAttackAbility>() != null)
			//{
			//	navMeshAgent.enabled = false;
			//}
		}
	}



	public override void BeforeAbility()
	{
		wayPointIndex = 0;
		reached = false;
		currentWaypoint = wayPoints[Random.Range(0, wayPoints.Count)];

		NavMesh.CalculatePath(transform.position, currentWaypoint.position, NavMesh.AllAreas, path);
		if (path.corners.Length != 0)
		{
			blockedPos = path.corners;
			navMeshAgent.SetDestination(blockedPos[wayPointIndex]);
			navMeshAgent.speed = finalSpeed;
			randomTime = Random.Range(4.0f, 15.0f);
			if (navMeshAgent.hasPath)
			{
				StartCoroutine(Timer(randomTime));
			}
			else
			{

			}
		}

	}

	public override void WhileAbility()
	{
		navMeshAgent.speed = finalSpeed;
		float dist = navMeshAgent.remainingDistance;
		if (dist != Mathf.Infinity && navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && navMeshAgent.remainingDistance <= 2)
		{
			
			if (wayPointIndex < blockedPos.Length)
			{
				navMeshAgent.SetDestination(blockedPos[wayPointIndex]);            
                wayPointIndex++;
			}
			else
			{

				AfterAbility();
			}
		}
	}

	public override void AfterAbility()
	{
		reached = true;
		BeforeAbility();
	}
	IEnumerator Timer(float time)
	{
		yield return new WaitForSeconds(time);
		if (Random.Range(0, 2) == 1 && randomStopMoment && finalSpeed != runSpeed)
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
	IEnumerator IdleTime(float time)
	{
		yield return new WaitForSeconds(time);
		navMeshAgent.speed = speed;
		AfterAbility();
	}
}

