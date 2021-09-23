using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject grenadePrefab;
    private GameObject grenadeSpawner, grenadeSpawner2;

    public int seccondsGrenadeInterval, seccondsGrenadeRange;

    [Range(0, 20)]
    public float grenadeHeight, grenadeWith;

    private void Start()
    {
        //Gets the location from the 2 objects at start.
        grenadeSpawner = GameObject.Find("GrenadeSpawn");
        grenadeSpawner2 = GameObject.Find("GrenadeSpawn_2");
        StartCoroutine(SpawnGrenade());
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    //The grenade will be spawned in random times, on the X axis between the grenadeSpawners.
        //    var position = new Vector3(Random.Range(grenadeSpawner.transform.position.x, grenadeSpawner2.transform.position.x), 1.5f, grenadeSpawner.transform.position.z);
        //    Instantiate(grenadePrefab, position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(new Vector3(0, grenadeHeight, grenadeWith) * 2500 * Time.deltaTime);
        //}
    }

    //Granade spawner
    private IEnumerator SpawnGrenade()
    {
        int interval;

        //The grenade will be spawned in random times, on the X axis between the grenadeSpawners.
        var position = new Vector3(Random.Range(grenadeSpawner.transform.position.x, grenadeSpawner2.transform.position.x), 1.5f, grenadeSpawner.transform.position.z);
        Instantiate(grenadePrefab, position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(new Vector3(0, grenadeHeight, grenadeWith) * 2500 * Time.deltaTime);

        //calculating the random amount of secconds to wait for a new grenade.
        interval = Random.Range(seccondsGrenadeInterval, seccondsGrenadeRange);
        yield return new WaitForSeconds(interval);

        StartCoroutine(SpawnGrenade());
    }
}
