using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))] 

public class IKsaber : MonoBehaviour {
    
    protected Animator animator;
    
    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj = null;
    public Transform cameraRig;
    public Transform forceHand;

    void Start () 
    {
        animator = GetComponent<Animator>();
    }
    
   
    void OnAnimatorIK()
    {
        if(animator) {
            
             
            if(ikActive) {

                
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }    

                
                if(rightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
                    animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
                }

                if (forceHand != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, forceHand.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, forceHand.rotation);
                }


                if (cameraRig != null)
                {

                    animator.SetBoneLocalRotation(HumanBodyBones.Spine, cameraRig.localRotation);
                }

            }
            
           
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
                animator.SetLookAtWeight(0);
            }
        }
    }    
}