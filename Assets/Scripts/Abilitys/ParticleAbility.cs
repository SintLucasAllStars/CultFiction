using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAbility : Ability, IPlayerAbilitys {
	[SerializeField]
	ParticleSystem runParticle;

	CharacterController _characterController;
	// Use this for initialization
	public override void  OnStart () {
		_characterController = GetComponent<CharacterController>();

        _characterController.callEveryFrame += EveryFrame;
	}

	public override void EveryFrame () {
		if(AbilityPermitted){
			WhileAbility();
		}
	}
	public override void BeforeAbility()
    {
    }

    public override void WhileAbility()
    {
		if (runParticle != null)
		{
			if (_characterController.playerSpeed > 5 && _characterController.grounded)
			{
				runParticle.enableEmission = true;
			}
			else
			{
				runParticle.enableEmission = false;
			}
		}
    }

    public override void AfterAbility()
    {

    }
}
