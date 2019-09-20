using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private GameManager gm;
    public List<GameObject> unitMoveSet;
    public GameObject unitSelectionUi;

    public List<GameObject> generalUi;

    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < generalUi.Count; i++)
        {
            generalUi[i].SetActive(false);
        }
        for (int i = 0; i < unitMoveSet.Count; i++)
        {
            unitMoveSet[i].SetActive(false);
        }
        gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnitSelectButton(int unitID)
    {
        if (CheckPhase(gm.gamePhase))
        {
            gm.selectedUnitToPlace = gm.unitDataBase[unitID];
        }

        gm.gamePhase = GameManager.Phase.SpawningPlayerUnits;
        Debug.Log("unit selected");
    }

    public void ConfirmButton()
    {
        EndTurn();
    }

    public bool CheckPhase(GameManager.Phase currentPhase)
    {
        if (currentPhase == GameManager.Phase.SelectingPlayerUnit)
        {
            return true;
        }

        return false;
    }

    public void EndTurn()
    {
        if (gm.gamePhase == GameManager.Phase.SpawningPlayerUnits)
        {
            gm.gamePhase = GameManager.Phase.SelectingAiUnit;
        }

        if (gm.gamePhase == GameManager.Phase.SelectPlayerUnitAction)
        {
            for (int i = 0; i < gm.blueTeam.Count; i++)
            {
                gm.blueTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.Active;
            }

            for (int i = 0; i < gm.redTeam.Count; i++)
            {
                gm.redTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.Inactive;
            }
            gm.gamePhase = GameManager.Phase.BattleAi;
        }
        
        gm.PhaseLoop();
    }

  

    public void StormTrooperAction(int action)
    {
        // stormTrooper
        
        // move
            if (action == 0)
            {
                gm.selectedActiveUnit.GetComponent<StormTrooper>().Move();
            }
        //shoot
            if (action == 1)
            {
                gm.selectedActiveUnit.GetComponent<StormTrooper>().Shoot();
            }
    }
    
    
    
    
}
