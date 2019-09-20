using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterController : MonoBehaviour
{
    public Camera playerCamera;
    Rigidbody rb;
    CapsuleCollider playerColl;
    float playerHeight;
    public float test;
    public enum movementState {Walking,Sprinting, Crouching,Slide};
    public movementState movState;
    

    float currentMoveSpeed;
    public float moveSpeed;
    public float slideSpeed;
    float maxSlideSpeed;
    public float sprintMultiplier;
    public float crouchMultiplier;
    public float jumpForce;
    public float senstivity;
    float xRot;
    public float maxXRot;
    public float minXRot;
    public bool isDead;

    #region Input Bools
    // Booleans for to update input next update (against input lag) \\
    bool moveNextUpdate;
    bool rotateNextUpdate;
    bool crouchNextUpdate;
    bool JumpNextUpdate;
    bool sprintNextUpdate;
    bool crouching;
    bool sliding;
    bool Grounded;
    bool sprinting;
    bool inTheAir;
    bool resetSpeed;
    bool resetHeight;
    #endregion

    private void Start()
    {
        maxSlideSpeed = slideSpeed;
        rb = GetComponent<Rigidbody>();
        playerColl = GetComponent<CapsuleCollider>();
        playerHeight = playerColl.height;
        currentMoveSpeed = moveSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        switch (movState)
        {
            case movementState.Walking:
                ResetHeight();
                ResetSpeed();
                sliding = false;
                crouching = false;
                break;
            case movementState.Sprinting:
                Sprint();
                break;
            case movementState.Crouching:
                Crouch();
                sliding = false;
                break;
            case movementState.Slide:
                Slide();
                break;
        }
        #region Controls Inputs
        if(Input.GetButton("Horizontal")||Input.GetButton("Vertical"))
        {
            moveNextUpdate = true;
        }
        if(Input.GetAxis("Mouse X") !=0 || Input.GetAxis("Mouse Y") !=0)
        {
            rotateNextUpdate = true;
        }
        if(Input.GetButton("Jump"))
        {
            JumpNextUpdate = true;
        }
        if (Input.anyKey && isDead)
        {
            Restart();
        }

        if(Input.GetButton("Crouch"))
        {
            if (movState == movementState.Sprinting||sliding)
            {
                movState = movementState.Slide;
            }
            else if(movState == movementState.Walking || crouching)
            {
                movState = movementState.Crouching;
            }
        }
        else if(Input.GetButton("Sprint"))
        {
            movState = movementState.Sprinting;
        }
        else
        {
            movState = movementState.Walking;
        }
        #endregion
        #region Input Bools
        if (moveNextUpdate)
        {
            Move();
            moveNextUpdate = false;
        }
        if (rotateNextUpdate)
        {
            Rotate();
            rotateNextUpdate = false;
        }
        if (JumpNextUpdate)
        {
            Jump();
            JumpNextUpdate = false;
        }
        #endregion

        if (sliding)
        {
            slideSpeed = slideSpeed - 0.2f;
            slideSpeed = Mathf.Clamp(slideSpeed,0,maxSlideSpeed);
            if(slideSpeed == 0)
            {
                movState = movementState.Crouching;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("JumpAble"))
        {
            Grounded = true;
            inTheAir = false;
        }
    }
    
    void Move()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 velocity = (movHorizontal + movVertical).normalized * currentMoveSpeed;
        
        rb.MovePosition(transform.position + velocity * Time.deltaTime);
    }
    void Rotate()
    {
        float yRot = Input.GetAxisRaw("Mouse X");
        xRot += -Input.GetAxisRaw("Mouse Y") * senstivity;
        xRot = Mathf.Clamp(xRot , minXRot, maxXRot);

        test = xRot;

        transform.Rotate(0, yRot * senstivity, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }
    void Jump()
    {
        if (Grounded && crouching == false)
        {
            Grounded = false;
            rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            inTheAir = true;
        }
    }
    void Crouch()
    {
        playerColl.height = playerColl.height / 2;
        currentMoveSpeed = moveSpeed / 2;
        crouching = true;
    }
    void Slide()
    {

        playerColl.height = playerColl.height / 2;
        currentMoveSpeed = slideSpeed;
        sliding = true;
    }
    void Sprint()
    {
        ResetHeight();
        ResetSpeed();
        currentMoveSpeed = moveSpeed * sprintMultiplier;
    }
    void ResetHeight()
    {
        playerColl.height = playerHeight;
    }
    void ResetSpeed()
    {
        currentMoveSpeed = moveSpeed;
        slideSpeed = maxSlideSpeed;
    }
    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
