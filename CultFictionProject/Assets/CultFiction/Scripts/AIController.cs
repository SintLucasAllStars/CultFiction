using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIAttackState
{
    Punch,
    Weapon
}

public class AIController : MonoBehaviour
{
    //private
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Animator RightLegAnim = null;
    [SerializeField]
    private Animator LeftLegAnim = null;
    [SerializeField]
    private Rigidbody rightArm;
    [SerializeField]
    private Rigidbody leftArm;

    //Public
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        
        Vector3 vel = (agent.nextPosition - transform.position) * 2;
        gameObject.GetComponent<Rigidbody>().velocity = vel ;

        if (!Vector3.Equals(agent.velocity, Vector3.zero))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(-agent.velocity.z, 0, agent.velocity.x)), 0.15F);
            RightLegAnim.SetBool("IsWalking", true);
            LeftLegAnim.SetBool("IsWalking", true);
        }
        else
        {
            RightLegAnim.SetBool("IsWalking", false);
            LeftLegAnim.SetBool("IsWalking", false);
        }

        if(agent.remainingDistance < 10)
        {
            punch();
        }
    }

    void punch()
    {
        leftArm.AddForce(transform.right * 100);
        rightArm.AddForce(transform.right * 100);
    }

}
