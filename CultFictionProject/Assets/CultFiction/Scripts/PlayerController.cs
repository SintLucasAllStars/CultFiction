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
   //public 
   public Transform GunLoc = null;
   public Gun currentGun;

   //private
   private Rigidbody rb = null;

   [SerializeField]
   private float speed = 0;
   [SerializeField]
   private GameObject Hand = null;
   [SerializeField]
   private GameObject hips = null;
   [SerializeField]
   private Animator RightLegAnim = null;
   [SerializeField]
   private Animator LeftLegAnim = null;
   [SerializeField]
   private PickUpTrigger pickUpTrigger = null;
   [SerializeField]
   private HingeJoint upperArm;
   [SerializeField]
   private HingeJoint LowerArm;
   [SerializeField]
   private int Health;

   private void Awake()
   {
        rb = GetComponent<Rigidbody>();
        
   }

   private void FixedUpdate()
   {
        Move();

        if (Input.GetMouseButtonDown(0) && !currentGun)
        {
            LowerArm.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 1500);
           
        }
   }

   private void Update()
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
                currentGun.Fire();
            }
        }

    }

   private void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 Movement = new Vector3(hAxis * speed, 0, vAxis * speed);
        rb.AddForce(Movement);

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

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * 1350);
        }
    }

   public void SetArmPos(ArmPoses newArmPose)
    {

        var upperjs = upperArm.spring;
        var lowerjs = upperArm.spring;
       

        switch (newArmPose)
        {
            case ArmPoses.None:
                upperjs.targetPosition = 0;
                upperArm.spring = upperjs;
                lowerjs.targetPosition = 0;
                LowerArm.spring = lowerjs;
                break;
            case ArmPoses.HoldGun:
                upperjs.targetPosition = 50;
                upperArm.spring = upperjs;
                lowerjs.targetPosition = 90;
                LowerArm.spring = lowerjs;
                break;
            case ArmPoses.Punch:
                upperjs.targetPosition = 90;
                upperArm.spring = upperjs;
                lowerjs.targetPosition = 90;
                LowerArm.spring = lowerjs;
                break;
            default:
                break;
        }
    }

    
    
}
