using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutSpawner : MonoBehaviour
{
    public GameObject coconutPrefab;
    public Vector3 spawnPos;
    public static GameObject mainCamera;
    private float cooldownTimer;

    void Start()
    {
        spawnPos = new Vector3(-7.86f, 1.76f, -6.54f);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0)
        {
            cooldownTimer = 0.3f;
            Instantiate(coconutPrefab, spawnPos, transform.rotation);
        }
    }
}
