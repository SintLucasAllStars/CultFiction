using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtonManager : MonoBehaviour
{
    private GameManager gm;
    public List<GameObject> unitMoveSet;
    // Start is called before the first frame update
    void Start()
    {
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

        if (gm.gamePhase == GameManager.Phase.BattlePlayer)
        {
            for (int i = 0; i < gm.blueTeam.Count; i++)
            {
                gm.blueTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.active;
            }

            for (int i = 0; i < gm.redTeam.Count; i++)
            {
                gm.redTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.inactive;
            }
            gm.gamePhase = GameManager.Phase.BattleAi;
        }
        
        gm.PhaseLoop();
    }
}
