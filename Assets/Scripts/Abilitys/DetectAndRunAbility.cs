using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndRunAbility : Ability, IPlayerAbilitys
{

    CharacterController _characterController;
	Transform player;
	WayPointWalkAbility wayPointWalkAbility; 
	[SerializeField]
	float viewRange = 10;
	[SerializeField]
	LayerMask thingsToSee;
	RaycastHit hit;
	[HideInInspector]
	public bool sawPlayer = false;
    // Use this for initialization
	public override void  OnStart()
	{
        _characterController = GetComponent<CharacterController>();
		_characterController.callEveryFrame += EveryFrame;
		wayPointWalkAbility = GetComponent<WayPointWalkAbility>();
		player = GameController.Instance.player.transform;
    }

	public override void  EveryFrame()
	{
        if (AbilityPermitted)
		{
			if(Vector3.Distance(transform.position,player.position)<viewRange){
				BeforeAbility();
			}else{
				if (sawPlayer)
                {
                    AfterAbility();
                }
			}

        }
    }
    public override void BeforeAbility()
    {
		Ray ray = new Ray(transform.position + (Vector3.up / 2),   player.position-transform.position);

		Debug.DrawRay(transform.position + (Vector3.up / 2), player.position - transform.position, Color.blue, viewRange, false);
		if (Physics.Raycast(ray, out hit, viewRange, thingsToSee))
        {
            WhileAbility();
		}else{
			if (sawPlayer)
            {
                AfterAbility();
            }
		}
    }

    public override void WhileAbility()
    {
		sawPlayer = true;
		wayPointWalkAbility.finalSpeed = wayPointWalkAbility.runSpeed;
    }

    public override void AfterAbility()
	{
		sawPlayer = false;
        wayPointWalkAbility.finalSpeed = wayPointWalkAbility.speed;

    }
}
