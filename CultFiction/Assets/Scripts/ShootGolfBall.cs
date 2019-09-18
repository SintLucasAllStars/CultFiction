using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGolfBall : MonoBehaviour
{
    public GameObject currentBall;
    public GameObject spawnpoint;
    public GameObject golfball;
    Vector3 qrntPos;
    Quaternion qrntRot;
    public GameObject club;
    public int multiplier;
    public float maxStrength;
    public float minStrength;
    public float power;
    Vector3 shootPos;
    public GameObject RotatePoint;
    public GameObject dirPos;
    float rot = -1;
    // Start is called before the first frame update
    void Start()
    {
        Respawn();
        SavePos();
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the club around but causes issues
        if (Input.GetKey(KeyCode.A))
        {
            SavePos();
            RotateClub(1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            SavePos();
            RotateClub(-1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Power();
            //orPos = club.transform.position;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //club.transform.position = orPos;
            club.transform.position = qrntPos;
            club.transform.rotation = qrntRot;
            Shoot(power);
            power = 0;
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

    private void SavePos()
    {
        qrntPos = club.transform.position;
        qrntRot = club.transform.rotation;
    }

    private void RotateClub(int i)
    {
        RotatePoint.transform.Rotate(new Vector3(0, i), Space.World);
    }

    private void Power()
    {
        if (power >= maxStrength)
        {
            power = maxStrength;
            rot = 0;
        }
        else
        {
            power += multiplier * Time.deltaTime;
            rot = -1;
        }
        club.transform.Rotate(new Vector3(rot, 0), Space.Self);
    }
}