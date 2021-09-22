using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rbPlayer;
    public float movementSpeed = 1f;
    private Vector3 moveDirection = Vector3.zero;

    private Camera cam;

    private void Start()
    {
        //Gets the rigidbody on start.
        rbPlayer = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    private void Update()
    {
        //sets the moveDirection to the input that has been given by the player.
        //GetAxisRaw = doesn't add the force up.
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //Moves the rigidbody.
        rbPlayer.transform.Translate(moveDirection * Time.deltaTime* movementSpeed);
        rbPlayer.transform.rotation = Quaternion.Euler(0,cam.transform.eulerAngles.y,0);
    }
}
