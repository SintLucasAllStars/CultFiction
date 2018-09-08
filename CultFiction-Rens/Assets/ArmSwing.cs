using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSwing : MonoBehaviour
{
	private Animation _animation;
	private AnimationState _animationState;

	private bool isSwinging = true;
	
	void Start ()
	{
		_animation = GetComponent<Animation>();
		_animationState = _animation["GolfSwing"];
	}

	private void OnEnable()
	{
		PlayerController.onHitBall += OnHitBall;
	}

	private void OnDisable()
	{
		PlayerController.onHitBall -= OnHitBall;
	}

	private void OnHitBall()
	{
		isSwinging = false;
		_animation.CrossFade("GolfHit",0.1f);
	}

	public void ResetAnimation()
	{
		_animation.Play("GolfSwing");
		isSwinging = true;
	}

	void Update ()
	{
		if(isSwinging)
		_animationState.time = PlayerController.Instance.Power;
	}
}
