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

    
    public UiButtonManager uiManager;

    // Start is called before the first frame update
    private void Awake()
    {
        unitPoints = 10;
    }

    void Start()
    {
        gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
        uiManager = GameObject.Find("Game Managers and debug").GetComponent<UiButtonManager>();
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
        //Input categorised by gamephase

        //Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            /*if (gm.gamePhase == GameManager.Phase.SelectingPlayerUnit)
            {
                // mask = LayerMask.NameToLayer("Unit Selection");
                // make sure a unit is selected to be spawned for next phase
                if (SelectionRaycast())
                {
                    Debug.Log("select unit");
                    gm.gamePhase = GameManager.Phase.SpawningPlayerUnits;
                }
                else
                {
                    Debug.Log("didnt select unit");
                }

                //Debug.Log(mask.value);
                return;
            }
*/

            if (gm.gamePhase == GameManager.Phase.SpawningPlayerUnits)
            {
                // mask =  LayerMask.NameToLayer("Player Unit Spawns");
                if (SelectionRaycast())
                {
                    GameObject unit = gm.selectedUnitToPlace;
                    // in selectionraycast bool select the unit you want to spawn. amd put it as parameter for placeunit
                    PlaceUnit(unit, selection.transform.position, unit.GetComponent<Soldier>().unitCost);
                    Debug.Log("placed unit");
                }
                else
                {
                    Debug.Log("Didnt place unit");
                }

                return;
            }
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

    bool SelectionRaycast()
    {
        // selection categorised by gamephase
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            selection = hit.collider.gameObject;
        }

        // for layer 9 select unit
        if (gm.gamePhase == GameManager.Phase.SelectingPlayerUnit)
        {
            if (selection.layer == LayerMask.NameToLayer("Unit Selection"))
            {
                //select unit on selection object
               

                Debug.Log("just before true select unit");
                Debug.Log("layer 9 hit");

                return true;
            }
        }

        //for layer 8 player unit spawns

        if (gm.gamePhase == GameManager.Phase.SpawningPlayerUnits)
        {
            if (selection.layer == LayerMask.NameToLayer("Player Unit Spawns"))
            {
                // spawn unit on location 
                Debug.Log("just before true spawn unit");
                Debug.Log("layer 8 hit");

                return true;
            }
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

    public void SelectUnitButton()
    {
    }

    void ConfirmUnits()
    {
    }


}
