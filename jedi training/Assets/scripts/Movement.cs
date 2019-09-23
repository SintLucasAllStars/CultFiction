using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator anim;
    public float movementSpeed;
    Vector3 directionalSpeed;
    public Transform boddy;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 directionalSpeed = new Vector3 (0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        int walkAnim = 0;

        if (Input.GetKey(KeyCode.W)){
            directionalSpeed.z = movementSpeed;
            walkAnim = 1;
        } else if (Input.GetKey(KeyCode.S))
        {
            directionalSpeed.z = -movementSpeed;
            walkAnim = 2;
        } else
        {
            directionalSpeed.z = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            directionalSpeed.x = movementSpeed;
            walkAnim = 3;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            directionalSpeed.x = -movementSpeed;
            walkAnim = 4;
        }
        else
        {
            directionalSpeed.x = 0;
        }

        transform.Translate(directionalSpeed * Time.deltaTime);
        anim.SetInteger("walkMode", walkAnim);
        if (walkAnim == 0)
        {
            boddy.localPosition = new Vector3(0,0,0);
        }
    }
}
