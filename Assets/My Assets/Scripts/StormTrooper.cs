using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class StormTrooper : Soldier
{
    public override void Start()
    {
        base.Start();
    }

    public override void Move()
    {
        if (unitState == unitStatus.Selected)
        {
            if (hasMoved != true)
            {
                base.Move();
                Debug.Log("stormtrooper move action");
                unitState = unitStatus.DoingAction;
                StartCoroutine(WaitForActionEnd(0));
                // now make spaces you can move light up
 
            }
            else
            {
                Debug.Log("this unit already moved this turn");
            }
        }
    }

    public override void MoveConfirm()
    {
        base.MoveConfirm();
        // move here
        // selectionraycast is jut to get the space the unit want to go to
        gameObject.transform.position = playerObject.selection.transform.position;
        ocupiedSpace.GetComponent<GridSpace>().spaceMovable = true;
        ocupiedSpace = playerObject.selection;
        
        actionEnd = true;
    }

    public override IEnumerator WaitForActionEnd(int action)
    {
        // move action
        if (action == 0)
        {
            
            ///////// problem
            while (actionEnd != true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (playerObject.SelectionRaycast())
                    {
                        MoveConfirm();
                    }
                }
                yield return null;
            }
            
            hasMoved = true;
            if (CheckActionPoints())
            {
                unitState = unitStatus.Selected;
            }
            else
            {
                unitState = unitStatus.Inactive;
                gm.CheckTeam();
            }
        }

        // shoot action
        if (action == 1)
        {
            while (actionEnd != true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (playerObject.SelectionRaycast())
                    {
                        MoveConfirm();
                    }
                }
                yield return null;
            }
            hasShot = true;
            if (CheckActionPoints())
            {
                unitState = unitStatus.Selected;
                gm.gamePhase = GameManager.Phase.BattlePlayer;
            }
            else
            {
                unitState = unitStatus.Inactive;
                gm.CheckTeam();
            }

        }
        actionEnd = false;

    }

    
    
    public override void Select()
    {
        base.Select();
    }

    public override void Shoot(GameObject target)
    {
        base.Shoot(target);
    }
    
}
