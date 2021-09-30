using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //##MOVEMENT##
    private float walkSpeed = 10;

    private Vector3 velocity;
    private float gravity = -20f;
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    private float groundDistance = 0.4f;

    private bool isGrounded;
    private float jumpHeight = 4.3f;

    void FixedUpdate()
    {
        //MOVEMENT
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        //Debug.Log(h);
        //Debug.Log(v);

        Vector3 move = transform.right * h + transform.forward * v;

        controller.Move(move * walkSpeed * Time.deltaTime);

        //RUN
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkSpeed = 15;
        }
        else { walkSpeed = 10; }

        //VELOCITY
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //JUMP
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public bool IsPlayerGrounded()
    {
        return isGrounded;
    }
}
