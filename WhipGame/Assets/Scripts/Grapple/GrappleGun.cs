using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UIElements;

public class GrappleGun : MonoBehaviour
{
    public Rigidbody rb;
    public CharacterController controller;

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask grappleMask;
    public Transform gunTip, cam, player;
    private float maxDist = 100f;
    private SpringJoint joint;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        controller.enabled = false;
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxDist))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            
            joint.maxDistance = 4.5f;
            joint.minDistance = 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }

    void DrawRope()
    {
        //no grapple no line
        if (!joint) return;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }
    void StopGrapple()
    {
        //controller.enabled = true;
        lr.positionCount = 0;
        Destroy(joint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
