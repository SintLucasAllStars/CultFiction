using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtonManager : MonoBehaviour
{
    private GameManager gm;
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

    public void UnitPlacementConfirm()
    {
        gm.gamePhase = GameManager.Phase.SelectingAiUnit;
        gm.PhaseLoop();
    }

    public bool CheckPhase(GameManager.Phase currentPhase)
    {
        if (currentPhase == GameManager.Phase.SelectingPlayerUnit)
        {
            return true;
        }

        return false;
    }
}
