using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    public enum State {Up,Down};
    public State state;
    public float speed;
    public float timeToStart;
    float timeToWait;
    Transform goToPosition;
    Transform orgPosition;
    GameObject go;
    bool hasStarted;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        goToPosition = Instantiate(new GameObject(), new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z),Quaternion.identity).transform;
        orgPosition = Instantiate(new GameObject(), transform.position, Quaternion.identity).transform;
    }

    // Update is called once per frame
    void Update()
    {
        //print(Vector3.Distance(transform.position, goToPosition.position));
        timeToStart--;
        if(timeToStart < 0)
        {
            hasStarted = true;
        }
        
        if (hasStarted)
        {
            switch (state)
            {
                case State.Up:
                    transform.position = Vector3.Lerp(transform.position, goToPosition.position, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, goToPosition.position) < 0.01f)
                    {
                        state = State.Down;
                    }
                    break;
                case State.Down:
                    transform.position = Vector3.Lerp(transform.position, orgPosition.position, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, orgPosition.position) < 0.01f)
                    {
                        state = State.Up;
                    }
                    break;
            }
        }
    }
}
