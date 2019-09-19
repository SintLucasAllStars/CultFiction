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
    
    

    public override void Shoot()
    {
        if (hasShot != true)
        {
            base.Shoot();
            unitState = unitStatus.DoingAction;
            StartCoroutine(WaitForActionEnd(1));
            // now show shooting range
        }
        else
        {
            Debug.Log("unit has already shot this turn");
        }
    }
    
    
    public override void ShootConfirm()
    {
        base.ShootConfirm();
        Soldier instance;
        instance = playerObject.selection.GetComponent<Soldier>();
        instance.TakeDamage(2);
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
                if (gm.CheckTeam())
                {
                    gm.gamePhase = GameManager.Phase.BattleAi;
                }
                else
                {
                    gm.gamePhase = GameManager.Phase.BattlePlayer;
                }
            }
        }

        // shoot action
        if (action == 1)
        {
            while (actionEnd != true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    playerObject.SelectionRaycast();

                    if (playerObject.selection.layer == LayerMask.NameToLayer("Ai Unit"))
                    {
                        ShootConfirm();
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
                // checks if whole team has done its actions
                if (gm.CheckTeam())
                {
                    Debug.Log("nobody can do anymore actions");
                    Debug.Log("Switching to ai");
                    gm.gamePhase = GameManager.Phase.BattleAi;
                }
                else
                {
                    gm.gamePhase = GameManager.Phase.BattlePlayer;
                }
            }
        }

        actionEnd = false;
    }

    
    
    public override void Select()
    {
        base.Select();
    }

    
    
}
