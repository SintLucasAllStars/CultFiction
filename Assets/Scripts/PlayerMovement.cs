using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gm;

    private float delayTime = .5f;

    private bool forward = true;
    private bool delay;

    void Start()
    {
        gm = GameManager.gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameStarted) MoveForward();
        
        if (Input.GetButtonDown("Jump") && !delay)
        {
            forward = !forward;
            if (float.Parse(string.Join("", gm.data.score)) < 50)
            {
                StartCoroutine(CreateDelay());
            }
        }

        if (transform.position.y < -1) die();
    }

    IEnumerator CreateDelay()
    {
        delay = true;
        yield return new WaitForSeconds(delayTime);
        delay = false;
    }

    void MoveForward()
    {
        transform.position += (forward? Vector3.forward * float.Parse(gm.data.StringListToFloat(gm.data.speed)) : Vector3.left * float.Parse(gm.data.StringListToFloat(gm.data.speed)) ) * Time.deltaTime;
    }

    void die()
    {
        gm.gameStarted = false;
        gameObject.transform.position = Vector3.up;
        forward = true;
        gm.DeathScreen();
    }
}
