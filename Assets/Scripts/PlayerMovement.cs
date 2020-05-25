using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float thrust = 5.0f;
    public float sideThrust = 4f;
    private Rigidbody rb;
    //private GameObject[] thrusters;
    private float smooth = 5f;
    public void Start()
    {
  //  thrusters = GameObject.FindGameObjectsWithTag("thruster");
       // print(thrusters);
    rb = gameObject.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        RaycastHit Hit;
        //raycast 
        if(Physics.Raycast(transform.position, Vector3.down, out Hit))
        {
           // print("object distance: " + Hit.distance);
            if(Hit.distance < 6)
            {
                // print("disctance is less then 2");
                //print("adding force to rigidbody: " + rb.transform.position);
                rb.AddForce(0, 10, 0);

            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * thrust);
            //foreach(GameObject T in thrusters)
            //{
            //    T.transform.rotation = Quaternion.Euler(gameObject.transform.position.x + 90, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
            //}
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.forward * -thrust);
            //foreach (GameObject T in thrusters)
            //{
            //    T.transform.rotation = Quaternion.Euler(gameObject.transform.position.x - 90, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
            //}
        }
        
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(transform.right * sideThrust);
        
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(transform.right * -sideThrust);


        if (Input.GetKey(KeyCode.Q))
            gameObject.transform.Rotate(0.0f, -2.0f, 0.0f);

        if (Input.GetKey(KeyCode.E))
            gameObject.transform.Rotate(0.0f, 2.0f, 0.0f);

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.other.tag == "lava")
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
