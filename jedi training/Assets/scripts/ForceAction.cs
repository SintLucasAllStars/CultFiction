using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAction : MonoBehaviour
{
    public Transform hand;
    Vector3 initialHandPos;
    public float handDistanceB;
    public float handMoveSpeedB;
    public float handDistanceF;
    public float handMoveSpeedF;
    public float retractDelay;
    float delayTimer = 0;
    bool attacking = false;
    bool waiting = false;
    bool retracting = false;
    bool canAttack = true;

    float forcePower = 0;
    public float forceMultypier;
    public float forceRange;
    public float forceWith;

    


    // Start is called before the first frame update
    void Start()
    {
        initialHandPos = hand.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 handPosB = new Vector3(initialHandPos.x, initialHandPos.y, initialHandPos.z - handDistanceB);
        Vector3 handPosF = new Vector3(initialHandPos.x, initialHandPos.y, initialHandPos.z + handDistanceF);

        if (canAttack && Input.GetMouseButton(1))
        {
            hand.localPosition = Vector3.MoveTowards(hand.localPosition, handPosB, handMoveSpeedB);
            if (hand.localPosition != handPosB)
            {
                forcePower++;
            }
        }

        if (canAttack && Input.GetMouseButtonUp(1))
        {
            attacking = true;
            canAttack = false;            
            RaycastHit hit;
            if (Physics.SphereCast(hand.position,forceWith, -hand.transform.TransformDirection(Vector3.up),out hit, forceRange))
            {
                Rigidbody rig = hit.transform.gameObject.GetComponent<Rigidbody>();
                if (rig != null)
                {
                    rig.velocity = -hand.transform.up * (forcePower * forceMultypier);
                }
            }
            

            forcePower = 0;
        }
        Debug.DrawRay(hand.position, -hand.transform.up*forceRange, Color.red);
        AttackAnim(handPosB, handPosF);
        

        

       
    }

    void AttackAnim(Vector3 HandposB, Vector3 HandPosF)
    {
        if (attacking)
        {
            hand.localPosition = Vector3.MoveTowards(hand.localPosition, HandPosF, handMoveSpeedF);
            if (hand.localPosition == HandPosF)
            {
                attacking = false;
                waiting = true;
            }
        }

        if (waiting)
        {
            if (delayTimer == 0)
            {
                delayTimer = Time.time + retractDelay;
            }
            if (Time.time > delayTimer)
            {
                waiting = false;
                retracting = true;
                delayTimer = 0;
            }
        }

        if (retracting)
        {
            hand.transform.localPosition = Vector3.MoveTowards(hand.transform.localPosition, initialHandPos, handMoveSpeedB*5);
            if (hand.transform.localPosition == initialHandPos)
            {
                retracting = false;
                canAttack = true;
            }
        }
    }
}
