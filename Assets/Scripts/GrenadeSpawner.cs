using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSpawner : MonoBehaviour
{
    public GameObject grenadePrefab;
    private GameObject grenadeSpawner, grenadeSpawner2;

    public int seccondsGrenadeInterval, seccondsGrenadeRange;

    [Range(0, 100)]
    public float grenadeHeight, grenadeWith;

    private void Start()
    {
        //Gets the location from the 2 objects at start.
        grenadeSpawner = GameObject.Find("GrenadeSpawn");
        grenadeSpawner2 = GameObject.Find("GrenadeSpawn_2");

        StartCoroutine(SpawnGrenade());
    }

    //Granade spawner
    private IEnumerator SpawnGrenade()
    {
        int interval;
        GameObject go;

        //The grenade will be spawned in random times, on the X axis between the grenadeSpawners.
        var position = new Vector3(Random.Range(grenadeSpawner.transform.position.x, grenadeSpawner2.transform.position.x), grenadeSpawner.transform.position.y, grenadeSpawner.transform.position.z);

        //This offset is needed to the whole trench is coverd in grenade fire.
        if (position.x <= -26)
        {
            Vector3 offsetZ = new Vector3(position.x, position.y, -30);

            go = Instantiate(grenadePrefab, offsetZ, Quaternion.identity);
        }
        else
        {
            go = Instantiate(grenadePrefab, position, Quaternion.identity);
        }
        
        Rigidbody rb = go.GetComponent<Rigidbody>();
        Vector3 force = new Vector3(0, grenadeHeight, grenadeWith) * 10;
        rb.AddForce(force);
        rb.AddRelativeTorque(new Vector3(10f, 0f, 0f));

        //Calculating the random amount of secconds to wait for a new grenade.
        interval = Random.Range(seccondsGrenadeInterval, seccondsGrenadeRange);
        yield return new WaitForSeconds(interval);

        StartCoroutine(SpawnGrenade());
    }
}
