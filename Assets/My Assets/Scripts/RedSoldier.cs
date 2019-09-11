using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class RedSoldier : Soldier
{
    
    void EndTurn()
    {
        if (gm.gamePhase == GameManager.Phase.BattleSideRed)
        {
            Debug.Log("RedSideTurn");
            gm.gamePhase = GameManager.Phase.SwitchSide;
        }

        gm.activeTeam = GameManager.ActiveTeam.BlueTeam;
    }
    
    void Update()
    {
        EndTurn();
    }

}
