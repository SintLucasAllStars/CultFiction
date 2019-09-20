using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private GameManager gm;
    public List<GameObject> unitMoveSet;
    public GameObject unitSelectionUi;
    public TextMeshProUGUI unitPointsDisplay;
    public List<GameObject> generalUi;
    public TextMeshProUGUI statusDisplay;

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

        unitSelectionUi.SetActive(false);
        gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
        UpdatePoints();
    }

    public void UpdatePoints()
    {
        unitPointsDisplay.text = "unitpoints: " + gm.playerInstance.unitPoints.ToString();
    }

    public void UpdateStatus(string newStatus)
    {
        statusDisplay.text = "status: \n" + newStatus;
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

        UpdateStatus("Now place unit on your side of the field");
        gm.gamePhase = GameManager.Phase.SpawningPlayerUnits;
        Debug.Log("");
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

        // move
        if (action == 0)
        {
            gm.selectedActiveUnit.GetComponent<StormTrooper>().Move();
            UpdateStatus("now select a space to where you want to go");
        }

        //shoot
        if (action == 1)
        {
            gm.selectedActiveUnit.GetComponent<StormTrooper>().Shoot();
            UpdateStatus("now select a enemy you want to shoot");
        }
    }
}
