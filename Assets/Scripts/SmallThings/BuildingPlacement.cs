using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour {
	public Transform CheckPos;
	public float length;
	public LayerMask thingsToGroundWith;
	public bool onStart;
	void Start(){
		if (onStart)
		{
			Invoke ("GetPos", 1);
		}
	}
	public float GetPos(){
		RaycastHit hit;
		Vector3 startRayPoint = CheckPos.position;
		Ray rayDown = new Ray (startRayPoint, -transform.up);
		Debug.DrawRay(startRayPoint, -transform.up, Color.yellow, length, false);
		if (Physics.Raycast (rayDown, out hit, length, thingsToGroundWith))
		{
			if (onStart)
			{
				transform.position = new Vector3 (transform.position.x, hit.point.y, transform.position.z);
			}
			return hit.point.y;

		}

		return 0f;
	}
}
