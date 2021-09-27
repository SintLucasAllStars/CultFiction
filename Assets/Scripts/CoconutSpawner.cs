using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutSpawner : MonoBehaviour
{
    public GameObject coconutPrefab;
    public Vector3 spawnPos;
    public static GameObject mainCamera;

    void Start()
    {
        spawnPos = new Vector3(-7.86f, 1.76f, -6.54f);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(coconutPrefab, spawnPos, transform.rotation);
        }
    }
}
