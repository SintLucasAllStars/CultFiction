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
            if (gm.gamePhase == GameManager.Phase.SelectingRedUnit)
            {
                mask = LayerMask.GetMask("Unit Selection");
                // make sure a unit is selected to be spawned for next phase
                if (SelectionRaycast(mask))
                {
                    
                }
            }

            if (gm.gamePhase == GameManager.Phase.SpawningRedUnits)
            {
                mask = LayerMask.GetMask("Player Unit Spawns");
                if (SelectionRaycast(mask))
                {
                    PlaceUnit(selectedUnitToPlace, selection.transform.position, selectedUnitToPlace.GetComponent<Soldier>().unitCost);
                }
                else
                {
                    Debug.Log("Didnt place unit");
                }
                
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

    bool SelectionRaycast(LayerMask selectionMask)
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        
        if (selectionMask == LayerMask.GetMask("Unit Selection"))
        {
            // select unit
            Debug.Log("just before true");

            return true;
        }
        
        
        //mask Player Unit Spawns
        if (selectionMask == LayerMask.GetMask("Player Unit Spawns"))
        {
            if (Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                selection = hit.collider.gameObject;
                //Debug.Log("Raycast void" + "" + selectionMask.ToString());
            } 
            // spawn unit on location 
            Debug.Log("just before true");

            return true;
        }
        
      
        
        
        
        //Debug.DrawRay(ray.origin, ray.direction, Color.red,20 );
        //Debug.Log("raycast void end");
        Debug.Log("just before false");
        return false;

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

   
}
