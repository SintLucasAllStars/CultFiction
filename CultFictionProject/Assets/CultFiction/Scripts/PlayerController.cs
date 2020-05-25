using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmPoses
{
    None,
    HoldGun,
    Punch
}


public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Gun currentGun;
    private Rigidbody rb = null;
    private bool canPunch;
    private bool isDead;

    [Header("Base Settings")]
    public int Health = 100;
    public int speed = 100;
    public Transform GunLoc = null;
    
    [SerializeField]
    private PickUpTrigger pickUpTrigger = null;
    
    
    //Joint Objects
    [Header(" Joint Objects & Animators")]
    [SerializeField]
    private GameObject upperArmR = null;
    [SerializeField]
    private GameObject lowerArmR = null;
    [SerializeField]
    private GameObject upperArmL = null;
    [SerializeField]
    private GameObject lowerArmL = null;
    [SerializeField]
    private GameObject LegL = null;
    [SerializeField]
    private GameObject LegR = null;
    [SerializeField]
    private Animator RightLegAnim = null;
    [SerializeField]
    private Animator LeftLegAnim = null;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Move();

            if (canPunch)
            {
                lowerArmR.GetComponent<Rigidbody>().AddForce(transform.right * 1500);
                canPunch = false;
            }
        }
    }

    private void Update()
    {
        if (!isDead)
        {
              
            if (pickUpTrigger.interactableGun && Input.GetKey(KeyCode.E))
            {
                pickUpTrigger.interactableGun.Grab(this);
                SetArmPos(ArmPoses.HoldGun);
            
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (currentGun)
                {
                    currentGun.Use();
                }
                else
                {   
                    canPunch = true;
                }
            }
        }
    }

    //Methods

    //Player Movement
    private void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 Movement = new Vector3(hAxis * speed, rb.velocity.y, vAxis * speed);
        rb.velocity = Movement;

        if (hAxis > 0 || hAxis < 0 || vAxis > 0 || vAxis < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(-vAxis, 0, hAxis)), 0.15F);
            RightLegAnim.SetBool("IsWalking", true);
            LeftLegAnim.SetBool("IsWalking", true);
        }
        else
        {
            RightLegAnim.SetBool("IsWalking", false);
            LeftLegAnim.SetBool("IsWalking", false);
        }
    }

    //Set Arm Posistion by Spring Target Position
    public void SetArmPos(ArmPoses newArmPose)
    {
        var upperjs = upperArmR.GetComponent<HingeJoint>().spring;
        var lowerjs = upperArmR.GetComponent<HingeJoint>().spring;
       
        switch (newArmPose)
        {
            case ArmPoses.None:
                upperjs.targetPosition = 0;
                upperArmR.GetComponent<HingeJoint>().spring = upperjs;
                lowerjs.targetPosition = 0;
                lowerArmR.GetComponent<HingeJoint>().spring = lowerjs;
                break;
            case ArmPoses.HoldGun:
                upperjs.targetPosition = 50;
                upperArmR.GetComponent<HingeJoint>().spring = upperjs;
                lowerjs.targetPosition = 90;
                lowerArmR.GetComponent<HingeJoint>().spring = lowerjs;
                break;
            case ArmPoses.Punch:
                upperjs.targetPosition = 90;
                upperArmR.GetComponent<HingeJoint>().spring = upperjs;
                lowerjs.targetPosition = 90;
                lowerArmR.GetComponent<HingeJoint>().spring = lowerjs;
                break;
            default:
                break;
        }
    }

    public void DoDamage(int dmg)
    {
        Health -= dmg;
        Health = Mathf.Clamp(Health, 0, Health);
        if(Health <= 0)
        {
            GetComponent<HingeJoint>().connectedBody = null;
            isDead = true;
        }
    }

}
