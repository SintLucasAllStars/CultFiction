using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionBaseOld : MonoBehaviour
{
    [Header("Joint setting")]
    public bool enableCollision = true;
    public bool enableConfigureDistance = false;
    public float distance = 0f;

    public float dampingRatio = 1;
    public float frequency = 0.5f;

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " have been clicked");

        if(GetComponent<FixedJoint2D>() == null)
        {
            SpringJoint2D joint = gameObject.AddComponent<SpringJoint2D>();

            // setting up component
            joint.connectedBody = Holder.instance.GetComponent<Rigidbody2D>();
            joint.enableCollision = enableCollision;
            joint.autoConfigureDistance = enableConfigureDistance;
            joint.distance = distance;

            joint.dampingRatio = dampingRatio;
            joint.frequency = frequency;
        }
    }
}
