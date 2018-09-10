using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAbility : Ability, IPlayerAbilitys
{
	private CharacterController _playerController;
	[SerializeField]
	float speed = 1;
	[SerializeField]
	float runSpeed = 5;
	[SerializeField]
	float slowWalkSpeed = 5;
	[SerializeField]
	bool walkSideWays = false;
	[SerializeField]
	float turnSpeed = 1;
	public override void OnStart()
	{
		_playerController = GetComponent<CharacterController>();
	}

	public override void EveryFrame()
	{
		float x = InputManager.Instance.horizontalAxis;
		float z = InputManager.Instance.verticalAxis;
		if (AbilityPermitted)
		{
			Vector3 forge = new Vector3(0, 0, 0);
			if (_playerController.canWalkRight && x > 0)
			{
				if (!walkSideWays)
				{
					transform.Rotate(0, (x * turnSpeed * Time.deltaTime), 0);
					if (z == 0)
					{
						z += x;
					}
				}
				else
				{
					forge.x += x;
				}
			}
			if (_playerController.canWalkLeft && x < 0)
			{
				if (!walkSideWays)
				{
					transform.Rotate(0, (x * turnSpeed * Time.deltaTime), 0);
					if (z == 0)
					{
						z -= x;
					}
				}
				else
				{
					forge.x += x;
				}
			}
			if (_playerController.canWalkForward && z > 0)
			{
				forge.z += z;
			}
			if (_playerController.canWalkBack && z < 0)
			{
				forge.z += z;
			}
			float tSpeed = speed;
			if (InputManager.Instance.runButton)
			{
				tSpeed = runSpeed;
			}
			if (InputManager.Instance.slowWalkButton)
			{
				tSpeed = slowWalkSpeed;
			}
			if (z != 0&&!_playerController.stateLocked&&_playerController.grounded)
			{            
				_playerController.currentPlayerState = CharacterController.PlayerStates.moving;
			}
			_playerController.playerSpeed = tSpeed*forge.z;
			transform.Translate(forge * Time.deltaTime * tSpeed);
		}
	}

	public override void BeforeAbility()
	{

	}

	public override void WhileAbility()
	{

	}

	public override void AfterAbility()
	{

	}
}

