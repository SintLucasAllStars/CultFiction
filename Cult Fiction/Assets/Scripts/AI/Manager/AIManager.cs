using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<PlayerAI> playerAI;
    public List<PlayerAI> groupLeader;

    private int[] leaderInt;

    public int controlledAI = 0;

    private void Start()
    {
        int j = 5;
        int groupNumber = 0;

        groupLeader.Add(playerAI[0]);
        playerAI[0].SelectUnitType(0, groupNumber, this, true);
        for (int i = 1; i < playerAI.Count; i++)
        {
            if (j == i)
            {
                groupNumber++;
                groupLeader.Add(playerAI[i]);
                playerAI[i].SelectUnitType(i, groupNumber, this, true);
            }
            else
            {
                playerAI[i].SelectUnitType(i, groupNumber, this, false);
            }
        }
    }

    public void PlayerDeath(PlayerAI ai, bool isLeader)
    {
        playerAI.Remove(ai);
        if (controlledAI != 0)
        {
            controlledAI--;
        }
    }

    private void MoveSelectedUnit(PlayerAI selectedPlayer)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            selectedPlayer.MoveTo(hit.point);
            MoveRemainingUnits(hit.point, controlledAI);
        }
    }

    private void MoveRemainingUnits(Vector3 pos, int groupNumber)
    {
        for (int i = 0; i < playerAI.Count; i++)
        {
            if ((i == controlledAI) || (playerAI[i].baseGroupNumber != groupNumber))
            {
                //nothing;
            }
            else
            {
                Vector3 WalkPos = new Vector3(pos.x + Random.Range(-6, 6), pos.y, pos.z + Random.Range(-6, 6));

                playerAI[i].MoveTo(WalkPos);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MoveSelectedUnit(groupLeader[controlledAI]);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            {
                if (0 <= controlledAI - 1)
                {
                    controlledAI--;
                }
                else
                {
                    controlledAI = groupLeader.Count - 1;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (groupLeader.Count - 1 >= controlledAI + 1)
            {
                controlledAI++;
            }
            else
            {
                controlledAI = 0;
            }
        }
    }
}