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
    [Header("Base Settings")]

    [SerializeField]
    public GameObject player;
    
    public Gun gun;
    public Transform gunLoc;
    public GameState gameState;

    [SerializeField]
    private int Health;

    private bool canPunch = true;
    private bool canShoot = true;
    private Gun currentGun;
    private bool isDead = false;
  
   
    // AI Settings
    [Header("Ai Settings")]

    [SerializeField]
    private AIAttackState attackState = AIAttackState.Punch;
    [SerializeField]
    private NavMeshAgent agent = null;

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

  
    // Start is called before the first frame update
    void Start()
    {
        agent.updatePosition = true;
        agent.updateRotation = true;

        if(attackState == AIAttackState.Weapon)
        {
            SpawnWeapon();
            agent.stoppingDistance = 8;
            var upS = upperArmR.GetComponent<HingeJoint>().spring;
            upS.targetPosition = 50;
            upperArmR.GetComponent<HingeJoint>().spring = upS;

            var lwS = lowerArmR.GetComponent<HingeJoint>().spring;
            lwS.targetPosition = 90;
            lowerArmR.GetComponent<HingeJoint>().spring = lwS;
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
        lowerArmL.GetComponent<Rigidbody>().AddForce(transform.right * 1500);
        lowerArmR.GetComponent<Rigidbody>().AddForce(transform.right * 1500);
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
            gameState.CurrentEnemies--;
            StartCoroutine("DespawnTimer");
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
        yield return new WaitForSeconds(.5f);
        canPunch = true;
        canShoot = true;
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.transform.parent.gameObject);
    }

    
}