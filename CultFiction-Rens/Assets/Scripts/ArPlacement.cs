using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARSessionOrigin))]
public class ArPlacement : MonoBehaviour
{
	public GameObject objectToPlace;

	private bool _isPlaced;
	
	ARSessionOrigin m_SessionOrigin;
    
	static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
	void Awake()
	{
		m_SessionOrigin = GetComponent<ARSessionOrigin>();
	}

	void Update()
	{
		if(_isPlaced) return;
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (m_SessionOrigin.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
			{
				Pose hitPose = s_Hits[0].pose;

				objectToPlace.transform.position = hitPose.position;
				_isPlaced = true;
			}
		}
	}
}