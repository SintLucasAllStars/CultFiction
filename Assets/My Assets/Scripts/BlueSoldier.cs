using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSoldier : Soldier
{
    // Start is called before the first frame update
    void EndTurn()
    {
        if (gm.gamePhase == GameManager.Phase.BattleSideBlue)
        {
            Debug.Log("BlueSideTurn");
            gm.gamePhase = GameManager.Phase.SwitchSide;
        }

        gm.activeTeam = GameManager.ActiveTeam.RedTeam;
    }

    void Update()
    {
        if (gm.activeTeam == GameManager.ActiveTeam.BlueTeam)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                //unitStatus = unitStatus.selected
            }
        }
        else
        {
            EndTurn();
        }
    }
}
