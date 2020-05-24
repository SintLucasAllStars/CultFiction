using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysAnimator : MonoBehaviour
{

    [SerializeField]
    private HingeJoint hJoint = null;
    [SerializeField]
    private GameObject ObjectToCopy = null;
    private JointSpring jSpring;

    public bool xAxis = false;
    public bool yAxis = false;
    public bool zAxis = false;


    private void Awake()
    {
        jSpring = hJoint.spring;
    }

    private void Update()
    {
        CopyBone();
        hJoint.spring = jSpring;
    }


    void CopyBone()
    {
        if (hJoint)
        { 
            if (xAxis)
            {
                jSpring.targetPosition = ObjectToCopy.transform.localEulerAngles.x;
                if(jSpring.targetPosition > 180)
                    jSpring.targetPosition = jSpring.targetPosition - 360;

                jSpring.targetPosition = Mathf.Clamp(jSpring.targetPosition, hJoint.limits.min + 5, hJoint.limits.max + 5);
            }
            if (yAxis)
            {
               
                jSpring.targetPosition = ObjectToCopy.transform.localEulerAngles.y;
                if (jSpring.targetPosition > 180)
                    jSpring.targetPosition = jSpring.targetPosition - 360;

                jSpring.targetPosition = Mathf.Clamp(jSpring.targetPosition, hJoint.limits.min + 5, hJoint.limits.max + 5);
            }
            if (zAxis)
            {
             
                jSpring.targetPosition = ObjectToCopy.transform.localEulerAngles.z;
                if (jSpring.targetPosition > 180)
                    jSpring.targetPosition = jSpring.targetPosition - 360;

                jSpring.targetPosition = Mathf.Clamp(jSpring.targetPosition, hJoint.limits.min + 5, hJoint.limits.max + 5);            }
        }
    }


}
