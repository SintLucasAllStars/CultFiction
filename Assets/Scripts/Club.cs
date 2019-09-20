using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Club : MonoBehaviour
{
    public enum State { aiming, shooting, idle };
    public State currentState = State.aiming;

    public float power;
    public GameObject ball;
    public Vector3 tPos;

    public Text debugText;

    public Vector2 shot;

    public Vector3 hitPos = new Vector3(0,-2,0);

    void Update() {

        switch (currentState)
        {
            case State.aiming:
                Aiming();
                break;
            case State.shooting:
                Shoot();
                break;
            case State.idle:
                Idle();
                break;
            default:
                Debug.Log("Switch sent to Default");
                break;
        }

    }

    void HandleClub(Vector2 position, TouchPhase phase)
    {

        tPos = Camera.main.ScreenToWorldPoint(position);
        tPos.z = 0;
        transform.position = tPos;

        if (phase == TouchPhase.Ended)
        {
            shot = ball.transform.position - transform.position;
            shot.Normalize();

            shot *= Vector2.Distance(ball.transform.position, transform.position) * power;

            currentState = State.shooting;

        }
    }

    #region Player Controlling Methods
    void Aiming() {

        if (Input.GetButton("Fire1"))
        {
            HandleClub(Input.mousePosition, TouchPhase.Moved);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            HandleClub(Input.mousePosition, TouchPhase.Ended);
        }

        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            HandleClub(t.position, t.phase);
        }

    }

    void Shoot() {

        transform.position = Vector3.MoveTowards(transform.position, hitPos, 10);

        if (transform.position == hitPos)
        {
            ball.GetComponent<Rigidbody2D>().AddForce(shot);
            shot = Vector2.zero;
            currentState = State.idle;
            GameManager.instance.tries++;
        }

    }

    void Idle() {

        if (ball.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            currentState = State.aiming;
        }

    }

    #endregion

}