using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;

    [SerializeField]
    private float speed = 3f;

    private float time = .5f;

    private bool forward = true;
    private bool delay;

    // Update is called once per frame
    void Update()
    {
        if (gm.gameStarted) MoveForward();
        
        if (Input.GetButtonDown("Jump") && !delay)
        {
            forward = !forward;
            StartCoroutine(CreateDelay());
        }
        
        if (transform.position.y < -2) die();
    }

    IEnumerator CreateDelay()
    {
        delay = true;
        yield return new WaitForSeconds(time);
        delay = false;
    }

    void MoveForward()
    {
        transform.position += (forward? Vector3.forward * speed : Vector3.left * speed ) * Time.deltaTime;
    }

    void die()
    {
        gameObject.transform.position = Vector3.up;
        forward = true;
    }
}
