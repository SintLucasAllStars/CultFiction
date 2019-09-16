using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private GameManager gm;
    private int unitPoints = 10;
    public GameObject selection;
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
        GameObject selectedUnitToPlace = gm.unitDataBase[0];

        //moue input actions
        if (Input.GetMouseButtonDown(0))
        {
            LayerMask mask;
            SelectionRaycast();
            Debug.Log("input mouse buttons work");
            //normal soldier cost 1
            if (gm.gamePhase == GameManager.Phase.SelectingRedUnit)
            {
                mask = LayerMask.GetMask("Unit Selection");
                // make sure a unit is selected to be spawned for next phase
                   
            }

            if (gm.gamePhase == GameManager.Phase.SpawningRedUnits)
            {
                mask = LayerMask.GetMask("Player Unit Spawns");
                PlaceUnit(selectedUnitToPlace, selection.transform.position, selectedUnitToPlace.GetComponent<Soldier>().unitCost);  
            }
 
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

    void SelectionRaycast(LayerMask selectionMask)
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, Mathf.Infinity, mask))
        {
            selection = hit.collider.gameObject;
            Debug.Log("Raycast void player spawn layer detected");
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red,20 );
        //Debug.Log("raycast void end");

    }

    void PlaceUnit(GameObject unit, Vector3 spawnPos, int unitCost)
    {
        //Soldier unitScript = unit.GetComponent;
        if (unitPoints > 0)
        {
            Instantiate(unit, spawnPos, Quaternion.identity);
            unitPoints = unitPoints - unitCost;
        }
        Debug.Log("Placing unit");
    }

    void SpawnUnit(GameObject unit, Vector3 spawnLocation)
    {
        Instantiate(unit, spawnLocation, Quaternion.identity);
    }
}
