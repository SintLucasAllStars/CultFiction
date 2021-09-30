using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UIElements;

public class GrappleGun : MonoBehaviour
{
    public CharacterController controller;
    private PlayerMovement playerMove;

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask grappleMask;
    public Transform gunTip, cam, player;
    private float maxDist = 20f;
    private SpringJoint joint;
    private Rigidbody rb;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        playerMove = GetComponentInParent<PlayerMovement>();
        Debug.Log("playermove: " + playerMove);
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

        if (playerMove.IsPlayerGrounded() == true && !Input.GetMouseButton(0))
        {
            controller.enabled = true;
            Destroy(rb);
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {    
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxDist, grappleMask))
        {
            controller.enabled = false;

            grapplePoint = hit.point;

            if (rb != null)
            {
                rb = player.gameObject.GetComponent<Rigidbody>();            
            } else rb = player.gameObject.AddComponent<Rigidbody>();

            joint = player.gameObject.AddComponent<SpringJoint>();

            rb.constraints = RigidbodyConstraints.FreezeRotation;

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
