using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask WhatIsGrappleable;
    public GameObject player;
    private Rigidbody rb;

    public Transform GunTip, Camera, Player;
    private float maxDis = 100f;
    private SpringJoint joint; 

    private Vector3 currentGrapplePosition; 

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
            Debug.Log("Shooting...");
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopGrapple();
            Debug.Log("Releasing...");

        }
    }

    void LateUpdate()
    {
        DrawRope(); 
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.position, Camera.forward, out hit, maxDis, WhatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;

            float disFromPoint = Vector3.Distance(Player.position, grapplePoint);

            joint.maxDistance = disFromPoint * 0.8f;
            joint.minDistance = disFromPoint * 0.25f;

            joint.spring = 7.5f;
            joint.damper = 5f;
            joint.massScale = 10f;

            lr.positionCount = 2;
            currentGrapplePosition = GunTip.position;
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint); 
    }

    void DrawRope()
    {
        if (!joint)
        {
            return;
        }

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        {
            lr.SetPosition(0, GunTip.position);
            lr.SetPosition(1, currentGrapplePosition);
        }
    }
}
