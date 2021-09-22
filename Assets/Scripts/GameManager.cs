using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject grenadePrefab;
    private GameObject grenadeSpawner, grenadeSpawner2;

    [Range(0, 20)]
    public float grenadeHeight, grenadeWith;

    private void Start()
    {
        //Gets the location from the 2 objects at start.
        grenadeSpawner = GameObject.Find("GrenadeSpawn");
        grenadeSpawner2 = GameObject.Find("GrenadeSpawn_2");
        SpawnGrenade();
    }

    //Granade spawner
    private void SpawnGrenade()
    {
        //The grenade will be spawned in random times, on the X axis between the grenadeSpawners.
        var position = new Vector3(Random.Range(grenadeSpawner.transform.position.x, grenadeSpawner2.transform.position.x), 1.5f, grenadeSpawner.transform.position.z);
        Instantiate(grenadePrefab, position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(new Vector3(0, grenadeHeight, grenadeWith) * 1000 * Time.deltaTime);
    }
}
