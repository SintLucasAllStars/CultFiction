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
    [SerializeField]
    private int Health;
    private bool canPunch = true;
    private bool canShoot = true;

    private Gun currentGun;
    [SerializeField]
    private HingeJoint upperArm;
    [SerializeField]
    private HingeJoint LowerArm;


    //Public
    public GameObject player;
    public bool isDead = false;
    public Gun gun;
    public Transform gunLoc;
    public AIAttackState attackState;


    // Start is called before the first frame update
    void Start()
    {
        agent.updatePosition = true;
        agent.updateRotation = true;

        if(attackState == AIAttackState.Weapon)
        {
            SpawnWeapon();
            agent.stoppingDistance = 8;
            var upS = upperArm.spring;
            upS.targetPosition = 50;
            upperArm.spring = upS;

            var lwS = LowerArm.spring;
            lwS.targetPosition = 90;
            LowerArm.spring = lwS;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
          
            agent.SetDestination(player.transform.position);

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

            if(agent.remainingDistance < agent.stoppingDistance + 2 && canShoot && currentGun)
            {
                currentGun.Use();
                Debug.Log("pewPew");
                StartCoroutine("AttackDelay");
                canShoot = false;

            }
           
        }
       
    }

    private void FixedUpdate()
    {
        if (agent.remainingDistance < 10 && canPunch && attackState == AIAttackState.Punch)
        {
            punch();
        }
    }

    void punch()
    {
        leftArm.AddForce(transform.right * 1500);
        rightArm.AddForce(transform.right * 1500);
        StartCoroutine("AttackDelay");
        canPunch = false;
    }

    public void DoDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            GetComponent<HingeJoint>().connectedBody = null;
            agent.SetDestination(transform.position);
            RightLegAnim.SetBool("IsWalking", false);
            LeftLegAnim.SetBool("IsWalking", false);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            isDead = true;
        }
    }

    void SpawnWeapon()
    {
        var g = Instantiate(gun, gunLoc);
        g.transform.localPosition = Vector3.zero;
        g.transform.localEulerAngles = Vector3.zero;
        g.transform.localScale = new Vector3(2, 2, 2);
        currentGun = g.GetComponent<Gun>();
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1);
        canPunch = true;
        canShoot = true;
    }
}