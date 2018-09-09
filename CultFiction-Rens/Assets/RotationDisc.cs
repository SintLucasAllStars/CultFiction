using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotationDisc : MonoBehaviour, IDragHandler
{
	private RectTransform _meterRotation;
	
	void Start ()
	{
		_meterRotation = GetComponentsInChildren<RectTransform>()[1];
	}


	public void OnDrag(PointerEventData eventData)
	{
		Vector3 newVec = Vector3.Normalize(transform.position);
		float angle = Quaternion.FromToRotation(Vector3.up, Input.mousePosition - Vector3.zero).eulerAngles.z;
		float remappedRotation = angle.Remap(270, 360, 90, 0);
		_meterRotation.rotation = Quaternion.Euler(0, 0, -remappedRotation);
		PlayerController.Instance.ChangeRotation(angle.Remap(270, 360, -185, 185));
	}
}
