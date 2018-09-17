using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {
	[SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;
	[SerializeField]
    private Vector3 offsetRotation;
    [SerializeField]
    private Space offsetPositionSpace = Space.Self;
	[SerializeField]
	float rotateSpeed =8;
    [SerializeField]
    private bool lookAt = true;

    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
			Quaternion targetRotation = Quaternion.LookRotation((target.position+offsetRotation) - transform.position);

            // Smoothly rotate towards the target point.
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}
