using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	private Ability[] abilitys;
	[HideInInspector]
	public bool canWalkRight;
	[HideInInspector]
	public bool canWalkLeft;
	[HideInInspector]
	public bool canWalkForward;
	[HideInInspector]
	public bool canWalkBack;
	public bool grounded;

	[SerializeField]
	float playerHeight = 1;
	[SerializeField]
	float rayDistance = 1;
	[SerializeField]
	int rowsOfRays = 3;
	[SerializeField]
	float offsetFromBottom = 0.2f;
	[SerializeField]
	LayerMask thingsToCollideWith;
	[SerializeField]
	float groundedRayLength = 1;
	[SerializeField]
	LayerMask thingsToGroundWith;

	public enum PlayerStates { idle, moving, jump, die,attack };
	public PlayerStates currentPlayerState;
	[HideInInspector]
	public bool stateLocked = false;
	[HideInInspector]
	public float playerSpeed;
	Vector3 oldPos;
	// Use this for initialization
	void Start()
	{
		abilitys = GetComponents<Ability>();
		foreach (Ability a in abilitys)
		{
			a.OnStart();
		}
	}

	// Update is called once per frame
	void Update()
	{
		EveryFrame();
	}
	void EveryFrame()
	{

		CastRayCastToSides();
		CastRayCastToBackAndForward();
		CastRaycastToBottom();

		foreach (Ability a in abilitys)
		{
			a.EveryFrame();
		}
        
		if(transform.position.x == oldPos.x&&transform.position.z == oldPos.z&&grounded&&!stateLocked){
			currentPlayerState = PlayerStates.idle;
		}else{
			oldPos = transform.position;
		}
	}
	void CastRayCastToSides()
	{
		RaycastHit hit;
		Vector3 startRayPoint = new Vector3(transform.position.x, (transform.position.y - (playerHeight / 2)) + offsetFromBottom, transform.position.z);
		float stepHeight = (playerHeight - offsetFromBottom) / (rowsOfRays - 1);
		bool cantWalkLeft = false;
		bool cantWalkRight = false;

		for (int i = 0; i < rowsOfRays; i++)
		{

			Ray rayLeft = new Ray(startRayPoint, -transform.right);
			//Debug.DrawRay(startRayPoint, -transform.right, Color.green, rayDistance, false);
			if (Physics.Raycast(rayLeft, out hit, rayDistance, thingsToCollideWith))
			{
				cantWalkLeft = true;
			}
			Ray rayRight = new Ray(startRayPoint, transform.right);
			//Debug.DrawRay(startRayPoint, transform.right, Color.red, rayDistance, false);
			if (Physics.Raycast(rayRight, out hit, rayDistance, thingsToCollideWith))
			{
				cantWalkRight = true;
			}

			startRayPoint += new Vector3(0, stepHeight, 0);
		}
		canWalkLeft = !cantWalkLeft;
		canWalkRight = !cantWalkRight;
	}
	void CastRayCastToBackAndForward()
	{
		RaycastHit hit;
		Vector3 startRayPoint = new Vector3(transform.position.x, (transform.position.y - (playerHeight / 2)) + offsetFromBottom, transform.position.z);
		float stepHeight = (playerHeight - offsetFromBottom) / (rowsOfRays - 1);
		bool cantWalkForward = false;
		bool cantWalkBack = false;

		for (int i = 0; i < rowsOfRays; i++)
		{
			Ray rayForward = new Ray(startRayPoint, transform.forward);
			//Debug.DrawRay(startRayPoint, transform.forward, Color.black, rayDistance, false);
			if (Physics.Raycast(rayForward, out hit, rayDistance, thingsToCollideWith))
			{
				cantWalkForward = true;
			}
			Ray rayBack = new Ray(startRayPoint, -transform.forward);
			//Debug.DrawRay(startRayPoint, -transform.forward, Color.blue, rayDistance, false);
			if (Physics.Raycast(rayBack, out hit, rayDistance, thingsToCollideWith))
			{
				cantWalkBack = true;
			}

			startRayPoint += new Vector3(0, stepHeight, 0);
		}
		canWalkBack = !cantWalkBack;
		canWalkForward = !cantWalkForward;
	}
	void CastRaycastToBottom()
	{
		RaycastHit hit;
		Vector3 startRayPoint = new Vector3(transform.position.x, (transform.position.y - (playerHeight / 2)) + offsetFromBottom, transform.position.z);
		Ray rayDown = new Ray(startRayPoint, -transform.up);
		Debug.DrawRay(startRayPoint, -transform.up, Color.yellow, groundedRayLength, false);
		if (Physics.Raycast(rayDown, out hit, groundedRayLength, thingsToGroundWith))
		{
			grounded = true;
		}
		else
		{
			grounded = false;
		}
	}
}
