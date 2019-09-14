using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gm;
    private int unitPoints = 10;
    // Start is called before the first frame update
    private void Awake()
    {
        unitPoints = 10;
    }

    void Start()
    {
        gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnUnit(gm.unitDataBase[0],Vector3.zero);
        }
    }

    void SpawnUnit(GameObject unit, Vector3 spawnLocation)
    {
        Instantiate(unit, spawnLocation, Quaternion.identity);
    }
}
