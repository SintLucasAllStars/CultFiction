using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGolfBall : MonoBehaviour
{
    public GameObject currentBall;
    public GameObject spawnpoint;
    public GameObject golfball;
    Quaternion ClubOriginalRot;
    public GameObject club;
    public int multiplier;
    public float maxStrength;
    public float minStrength;
    public float power;
    Vector3 shootPos;
    public GameObject RotatePoint;
    public GameObject dirPos;
    // Start is called before the first frame update
    void Start()
    {
        Respawn();
        ClubOriginalRot = club.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Shootground")
                {
                    Debug.Log("Hit this point ");
                    Debug.Log(hit.point);
                    dirPos.transform.position = hit.point;
                }
                else
                {
                    Debug.Log("You have to choose a position on the shootground");
                }
            }
        }
        // Moves the club around but causes issues
        if (Input.GetKey(KeyCode.A))
        {
            RotatePoint.transform.Rotate(new Vector3(0, 1), Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            RotatePoint.transform.Rotate(new Vector3(0, -1), Space.World);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            float rot = -1;
            if(power >= maxStrength)
            {
                power = maxStrength;
                rot = 0;
            }
            else
            {
                power += multiplier * Time.deltaTime;
            }
            club.transform.Rotate(new Vector3(rot, 0), Space.Self);
            //orPos = club.transform.position;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //club.transform.position = orPos;
            club.transform.rotation = ClubOriginalRot;
            Shoot(power);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    public void Shoot(float strenght)
    {
        Vector3 dir;
        dir = currentBall.transform.position - dirPos.transform.position;
        dir.Normalize();
        currentBall.GetComponent<Rigidbody>().AddForce(dir * strenght, ForceMode.Impulse);
        power = 0;
        Destroy(currentBall, 5);
        currentBall = null;
        Respawn();
    }

    public void Respawn()
    {
        
        if (currentBall == null)
            currentBall = Instantiate(golfball, spawnpoint.transform.position, Quaternion.identity);
        else
            Debug.Log("There is already a ball noob shoot this one first");
    }
}