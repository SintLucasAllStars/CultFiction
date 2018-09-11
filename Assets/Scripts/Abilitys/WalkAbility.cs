using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAbility : Ability, IPlayerAbilitys
{
	private CharacterController _characterController;
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
		_characterController = GetComponent<CharacterController>();
		_characterController.callEveryFrame += EveryFrame;
	}

	public override void EveryFrame()
	{
		float x = InputManager.Instance.horizontalAxis;
		float z = InputManager.Instance.verticalAxis;
		if (AbilityPermitted)
		{
			Vector3 forge = new Vector3(0, 0, 0);
			if (_characterController.canWalkRight && x > 0)
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
			if (_characterController.canWalkLeft && x < 0)
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
			if (_characterController.canWalkForward && z > 0)
			{
				forge.z += z;
			}
			if (_characterController.canWalkBack && z < 0)
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
			if (z != 0&&!_characterController.stateLocked&&_characterController.grounded)
			{            
				_characterController.currentPlayerState = CharacterController.PlayerStates.moving;
			}
			_characterController.playerSpeed = tSpeed*forge.z;
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

