using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rbPlayer;
    public float movementSpeed = 1f, duckingspeed;
    private Vector3 moveDirection = Vector3.zero;

    private Camera cam;

    private Vector3 duckingHeight = new Vector3(0.5f,0.5f,0.5f), currentHeight, walkingHeight = new Vector3(0.75f,0.75f,0.75f);

    private GameManager _gameManager;

    private void Start()
    {
        //Gets the rigidbody on start.
        rbPlayer = GetComponent<Rigidbody>();
        cam = Camera.main;
        currentHeight = rbPlayer.transform.localScale;

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (currentHeight == duckingHeight)
        {
            _gameManager._isDucked = true;
        }
    }

    private void Update()
    {
        //sets the moveDirection to the input that has been given by the player.
        //GetAxisRaw = doesn't add the force up.
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //Moves the rigidbody.
        rbPlayer.transform.Translate(moveDirection * Time.deltaTime* movementSpeed);
        rbPlayer.transform.rotation = Quaternion.Euler(0,cam.transform.eulerAngles.y,0);


        //This will check the if the player is giving an input.
        //If the player is not moving the player will be in ducking position, if moving in walking mode.
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            //Debug.Log("i'm moving!");
            switch (currentHeight != walkingHeight)
            {
                case true:
                    //Switch to walkingheight
                    currentHeight = walkingHeight;
                    _gameManager._isDucked = false;
                    break;
                case false:
                    //nothing to do
                    break;
            }
        }
        else
        {
            //Debug.Log("I'm standing still");
            switch (currentHeight != duckingHeight)
            {
                case true:
                    //Switch to duckingheight
                    currentHeight = duckingHeight;
                    _gameManager._isDucked = true;
                    break;
                case false:
                    //nothing to do
                    break;
            }
        }

        //this will change the scale in a smooth way.
        transform.localScale = Vector3.Lerp (transform.localScale, currentHeight, duckingspeed * Time.deltaTime);

        _gameManager.ActivateSniper();
    }
}
