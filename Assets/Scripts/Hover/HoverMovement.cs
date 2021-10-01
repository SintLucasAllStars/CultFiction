using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : MonoBehaviour
{
    bool timerStart = false;

    [Header("Don't change")]
    public float curSpeed;
    public bool isHovering;
    public Vector3 curGrav;

    [Header("Settings")]
    public float losePer;
    public List<Thruster> thrusters;
    public List<Booster> boosters;
    public List<Rudder> rudders;
    public float hoverDist = 5f;
    public float pushForce = 250f;
    public float airGravity = 75f;
    public float hoverGravity = 60f;

    float reffer;
    Quaternion startPos;
    private float lerp;
    private Rigidbody rb;
    private RaycastHit hit;
    private Vector3 pMoveInput;

    void Start()
    {
        timerStart = false;
        rb = GetComponent<Rigidbody>();
        RotateRight(true);
    }

    void RotateRight(bool start = false)
    {
        
        if (start) { startPos = Quaternion.identity; }
        else
        {
            float curRot = transform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(startPos.x, transform.eulerAngles.y, startPos.z);
            rb.MoveRotation(targetRotation);
        }
    }

    private void Update()
    {
        if (pMoveInput != Vector3.zero && timerStart == false)
        {
            GameManager.instance.SetTimer();
            timerStart = true;
        }
    }

    void FixedUpdate()
    {
        curSpeed = Vector3.Dot(rb.velocity, transform.forward);
        GetPlayerInput();
        GroundDetection();
        RotateRight();
        AddForces();
    }


    void AddForces()
    {
        rb.velocity = rb.velocity * losePer;
        rb.angularVelocity = rb.angularVelocity * losePer;
        foreach (Thruster t in thrusters)
        {
            t.ThrustForce(rb);
        }
        foreach (Booster b in boosters)
        {
            b.BoostForce(rb, pMoveInput.y);
        }
        foreach (Rudder r in rudders)
        {
            r.TurnForce(rb, pMoveInput.x);
        }
        rb.AddForce(curGrav);

    }

    void GroundDetection()
    {
        Vector3 player = transform.position;

        if (Physics.SphereCast(player, player.y / 2, -transform.up, out hit))
        {
            if (hit.distance < hoverDist) isHovering = true;
            else isHovering = false;
        }

        if (isHovering) curGrav = -transform.up * hoverGravity;
        else curGrav = -transform.up * airGravity;
    }

    private void GetPlayerInput()
    {
        //rudder
        pMoveInput.x = Input.GetAxisRaw("Horizontal");
        //Thruster
        pMoveInput.y = Input.GetAxisRaw("Vertical");
        //pMoveInput.Normalize();
        pMoveInput = Vector3.ClampMagnitude(pMoveInput, 1);
    }
}
