using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;

    public float hoverDist;
    public float drag;
    public float gravity;
    public bool isGrounded;
    public float groundDist;

    public Vector3 velocity;
    private Vector2 pMoveInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = groundCheck();
        GetMoveInput();
        HandleGravity();
    }

    private void GetMoveInput()
    {
        pMoveInput.x = Input.GetAxisRaw("Horizontal");
        pMoveInput.y = Input.GetAxisRaw("Vertical");
        pMoveInput = Vector3.ClampMagnitude(pMoveInput, 1);
    }
    void HandleGravity()
    {
        if(gravity < 0)
        {
            velocity.y += gravity * Time.deltaTime;
            if (groundCheck())
            {
                velocity.y = -2f;
            }
        }
    }
    private bool groundCheck()
    {
        RaycastHit hit;

        Vector3 player = transform.position;

        if (Physics.SphereCast(player, player.y / 2, Vector3.down, out hit, groundDist))
        {
            return true;
        }
        else { return false; }
    }

}
