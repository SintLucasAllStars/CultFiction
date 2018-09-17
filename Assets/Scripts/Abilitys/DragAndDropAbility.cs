using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragAndDropAbility : Ability , IPlayerAbilitys {
	public float dragDistance = 2;
	public float dropDistance = 2;
	public LayerMask thingsToDrag;
	public LayerMask thingsToDropOn;
	public Vector3 dragOffset;
	RaycastHit hit;
	RaycastHit hit2;
	RaycastHit hitDrop;
	Vector3 closestPoint;
	Vector3 Pos;
	private CharacterController _playerController;
	Rigidbody rb;
	GameObject cameraChild;
	bool pickedUpObject;
	GameObject draggingObject;
	float storeDis;
	public string whenSeeingInteractible = "Press E to interact.";
	public Text textToshow;
	public float minDragDis =2;
	bool textSet;

	public override void OnStart(){
		_playerController = GetComponent<CharacterController> ();
		cameraChild = GetComponentInChildren<Camera> ().gameObject;
	}
	public override void EveryFrame(){
		if (AbilityPermitted)
		{
			Vector3 startRayPoint = cameraChild.transform.position;
			Ray rayInteract = new Ray (startRayPoint, cameraChild.transform.forward);
			Debug.DrawRay(startRayPoint, cameraChild.transform.forward, Color.cyan, dragDistance, false);
			if (!pickedUpObject)
			{
				if (Physics.Raycast (rayInteract, out hit, dragDistance, thingsToDrag))
				{
					if (textToshow != null && !textSet)
					{
						textToshow.text = whenSeeingInteractible;
						textSet = true;
					}
					if (InputManager.Instance.interactButton && _playerController.grounded)
					{
						if (textSet)
						{
							textSet = false;
							textToshow.text = "";
						}
						BeforeAbility ();
					}
				}
				else
				{
					if (textSet)
					{
						textSet = false;
						textToshow.text = "";
					}
				}
			}
			else
			{
				WhileAbility ();
			}
		}
	}

	public override void BeforeAbility(){
		draggingObject = hit.collider.gameObject;
		draggingObject.layer = 12;
		//hit.collider.isTrigger = true;
		pickedUpObject = true;
	}

	public override void WhileAbility(){

		Vector3 startRayPoint = cameraChild.transform.position;
		Ray rayInteract = new Ray (startRayPoint, cameraChild.transform.forward);
		Ray rayDrop = new Ray (draggingObject.transform.position, -transform.up);
		Debug.DrawRay(startRayPoint, cameraChild.transform.forward, Color.cyan, dropDistance, false);
		if (Physics.Raycast (rayInteract, out hitDrop, dropDistance, thingsToDropOn))
		{

		}
		if (Vector3.Distance (transform.position, hitDrop.point) > minDragDis)
		{
			draggingObject.transform.position = hitDrop.point + dragOffset;
		}

		Debug.DrawRay(draggingObject.transform.position, -transform.up, Color.yellow, dropDistance, false);
		if (Physics.Raycast (rayDrop, out hit2, dropDistance, thingsToDropOn))
		{
			if (InputManager.Instance.interactButton && _playerController.grounded)
			{
				closestPoint = hit.collider.ClosestPoint (hitDrop.point);


				Pos = draggingObject.transform.position - closestPoint;
				if (Pos.y > storeDis) {
					storeDis = Pos.y;
				}
				AfterAbility ();
			}
		}
	}

	public override void AfterAbility(){
		//draggingObject.GetComponent<Collider> ().isTrigger = false;
		draggingObject.layer = 11;
		draggingObject.transform.position = hitDrop.point + new Vector3(0,storeDis,0);
		draggingObject = null;
		pickedUpObject = false;
		if (textSet)
		{
			textSet = false;
			textToshow.text = "";
		}
	}
}
