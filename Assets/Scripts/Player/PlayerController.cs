using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rB;

    bool inFlight = true;
    float speed = 10;

    public GameObject leftWing, rightWing;

    public float mainPitch, lPitch, rPitch;
    public float mainRoll, lRoll, rRoll;
    public float mainYaw;

    #region Booleans used for cleanup of movement states
    bool down;
    bool up;
    bool right;
    bool left;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        down = Input.GetKey(KeyCode.W);
        up = Input.GetKey(KeyCode.S);
        right = Input.GetKey(KeyCode.A);
        left = Input.GetKey(KeyCode.D);

        if (inFlight)
        {
            //Forward Movement
            transform.position += Vector3.forward * speed * Time.deltaTime;
            transform.Rotate(mainPitch, mainYaw, mainRoll);

            //Movement Up/Down
            if (down && mainPitch <= 0.5f)
            {
                mainPitch += 0.01f;
            }
            if (up && mainPitch <= 0.5f)
            {
                mainPitch += -0.01f;
            }

            if (mainPitch >= 0 && !down && !up)
            {
                mainPitch -= 0.01f;
            }
            if (mainPitch <= 0 && !up && !down)
            {
                mainPitch -= -0.01f;
            }

            //Movement Left/Right
            if (left && mainRoll <= 0.5f && mainYaw <= 0.05f)
            {
                mainRoll += 0.01f;
                mainYaw += 0.001f;
                Debug.Log(mainRoll + "|" + mainYaw);
            }
            if (right && mainRoll >= -0.5f && mainYaw <= -0.05f)
            {
                Debug.Log("A is Pressed");
                mainRoll += -0.01f;
                mainYaw += -0.001f;
            }

            if (mainRoll >= 0 && !left)
            {
                mainRoll -= 0.01f;
                mainYaw -= 0.01f;
            }
            if (mainRoll <= 0 && !right)
            {
                mainRoll -= -0.01f;
                mainYaw -= -0.01f;
            }
            //while (mainRoll >= 0 && !Input.GetKey(KeyCode.D)){
            //        mainRoll += -0.01f;
            //        transform.Rotate(0, 0, mainRoll);
            //}
            

            //MoveWingsNormal();
        }
    }

    void MoveWingsNormal()
    {
        #region Old Mechanic
        /* Old, used to give a more Plane-like flight system
        float smooth = 5f;
        float tiltAngle = 60f;

        float rotateZ;
        float rotateX;
        float rotateY;
        
        //Flap Wings to gain speed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rB.velocity = new Vector3(0, 0, speed);
        }

        //Lean forward and backward to Dive and Rise
        rotateX = Input.GetAxis("Vertical") * tiltAngle;

        //Lean sideways to change direction
        rotateZ = Input.GetAxis("Horizontal") * tiltAngle;

        rotateY = 0;

        Quaternion target = Quaternion.Euler(90 + rotateX, rotateY, -rotateZ);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        */
        #endregion
        
    }
}
