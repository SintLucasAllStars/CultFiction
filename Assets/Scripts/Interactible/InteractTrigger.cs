using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractTrigger : MonoBehaviour , IInteractible{
	public bool needsButtonPress = true;
	public bool needItem =false;
	public int itemId = 0;
	public enum objectHandle{DisableObject,EnableObject,ToggelObject,PlayAnimation,ToggleAnimation};
	public objectHandle whatToDoWithObject;
	public string animationName;
	public string animationNameSecond;
	public GameObject[] objectsToChange;
	public IExtraFunction extraFunctionToCall;
	public Text interactText;
	public string failText;
	public void Interact(){
		extraFunctionToCall = gameObject.GetComponent<IExtraFunction> ();
		if (extraFunctionToCall!=null)
		{
			extraFunctionToCall.ExtraFunction ();
		}
		bool haveKey = false;
		if (!needItem)
		{
			SetObjects ();
		}
		else
		{
			
		}
	}
	void SetObjects(){
		foreach (GameObject g in objectsToChange)
		{
			switch (whatToDoWithObject)
			{
			case objectHandle.DisableObject:
				g.SetActive (false);
				break;
			case objectHandle.EnableObject:
				g.SetActive (true);
				break;
			case objectHandle.ToggelObject:
				g.SetActive (!g.activeSelf);
				break;

			case objectHandle.PlayAnimation:
				g.GetComponent<Animator> ().Play (animationName,0);
				break;

			case objectHandle.ToggleAnimation:

				Animator anim;
				anim = g.GetComponent<Animator> ();
				if (anim.GetCurrentAnimatorStateInfo (0).normalizedTime >1)
				{
					if (anim.GetCurrentAnimatorStateInfo (0).IsName (animationName))
					{
						anim.Play (animationNameSecond, 0);
					}
					else
					{
						anim.Play (animationName, 0);
					}
				}
				break;
			}
		}
	}
	void OnTriggerEnter(Collider other){
		if (!needsButtonPress)
		{
			Interact ();
		}
	}
}
