using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public float runSpeed;
    public float walkSpeed;
    public float jumpPower;
    public float gizmoSize;
    public float screenSpaceDistanceMultiplier;
    public float threshold=0.1f;

    public Rigidbody rigidBody;

    private Vector3 playerScreenPos;
    private Vector3 targetScreenPos;
    private Vector3 targetWorldSpacePos;
    

    private float curSpeed;
    private Animator animator;

    
     



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();  
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat("x",Input.GetAxis("Horizontal"));
        animator.SetFloat("z",Input.GetAxis("Vertical"));
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Moveplayer();
            RotatePlayer();
            animator.SetTrigger("startWalking");
        } else 
        {
            animator.SetTrigger("goIdle");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            LightAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            HeavyAttack();
        }

        
    }

    public void Moveplayer()
    {
        if(Input.GetAxis("Fire1") < 0.5f)
        {
            curSpeed = walkSpeed;

        } else {
            curSpeed = runSpeed;
        }

       // rigidBody.velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal")* playerSpeed, 0.8f), 0, Mathf.Lerp(0, Input.GetAxis("Vertical")* playerSpeed, 0.8f));

         
        playerScreenPos = cam.WorldToScreenPoint(transform.position);
        targetScreenPos = new Vector3(playerScreenPos.x + (Input.GetAxis("Horizontal") * screenSpaceDistanceMultiplier), playerScreenPos.y + (Input.GetAxis("Vertical") * screenSpaceDistanceMultiplier), 0);
       
            int layerMask = LayerMask.GetMask("Ignore Raycast");
            
            Debug.Log("Mask is: " + layerMask);
            layerMask = ~layerMask;
            Debug.Log("Inversed mask is: " + layerMask);
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(targetScreenPos);

            if (Physics.Raycast(ray,  out hit, 10000.0f, layerMask)) 
            {
               targetWorldSpacePos = hit.point;
               Debug.Log(hit.collider.gameObject.name);
            }
        
            //Debug.Log("target is " + targetScreenPos.x + " pixels from the left and" + targetScreenPos.y + "from the bottom.");
            //Debug.Log(targetWorldSpacePos);
            Vector3 tPositionCache = new Vector3(targetWorldSpacePos.x, 0, targetWorldSpacePos.z);
           // rigidBody.AddForce((tPositionCache - transform.position * playerSpeed));
           float step =  curSpeed * Time.deltaTime;
           if(Vector3.Distance(transform.position, targetWorldSpacePos) > threshold)
           {
               transform.position = Vector3.MoveTowards(transform.position, targetWorldSpacePos, step); 
           } 

           // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetWorldSpacePos, 1);
        
       
        
    }

    public void RotatePlayer()
    {
        float moveHorizontal = Input.GetAxisRaw ("Horizontal");
        float moveVertical = Input.GetAxisRaw ("Vertical");
  
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
    }

    public void LightAttack()
    {
        animator.SetTrigger("swordHit");
 
        
    }

    public void HeavyAttack()
    {
        
            animator.SetTrigger("swordHeavyhit");
        
    }



    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetWorldSpacePos, gizmoSize);
        Gizmos.DrawSphere(transform.position, gizmoSize);
    }

    private void OnTriggerEnter(Collider other) {
        
         if(other.gameObject.tag == "Enemy")
         {
             Destroy(other.gameObject);
         }

    }
}

