using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        //Debug.Log(Input.inputString);
        
        if (Input.anyKeyDown)
        {
            
            PlayerInputCheck(Input.inputString);   
        }
    }

    void PlayerInputCheck(String playerInput)
    {
        //moue input actions
        if (Input.GetMouseButtonDown(0))
        {
            SelectionRaycast();
            Debug.Log("input mouse buttons work");
            //SelectionRaycast();
        }

        //keyboard input actions (specific to upper and lower cased letters)
        if (playerInput == "h")
        {
            Debug.Log("H works");
        }
        if (playerInput == "r")
        {
            Debug.Log("R works");
        }
    }

    void SelectionRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Player Unit Spawns");
        if (Physics.Raycast(ray, Mathf.Infinity, mask))
        {
            Debug.Log("Raycast void player spawn layer detected");
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red,20 );
        Debug.Log("raycast void end");

    }

    void SpawnUnit(GameObject unit, Vector3 spawnLocation)
    {
        Instantiate(unit, spawnLocation, Quaternion.identity);
    }
}
