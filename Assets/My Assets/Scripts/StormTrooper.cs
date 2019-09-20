using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UIElements;
using Random = System.Random;

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
        gameObject.transform.position = playerInstance.selection.transform.position;
        ocupiedSpace.GetComponent<GridSpace>().spaceMovable = true;
        ocupiedSpace = playerInstance.selection;
        
        actionEnd = true;
    }

    public override void AiMoveConfirm(int gridMax, int gridMinimum)
    {
        Vector3 aiCurrentPos;
        int xMax;
        int xCurrent;
        int xMin;
        int zMax;
        int zCurrent;
        int zMin;
        int newId;

        aiCurrentPos = aiInstance.aiUnitInstance.ocupiedSpace.transform.position;
        xCurrent = Mathf.FloorToInt(aiCurrentPos.x);
        xMax = xCurrent + movementAllowance;
        xMin = xCurrent - movementAllowance;

        zCurrent = Mathf.FloorToInt(aiCurrentPos.z);
        zMax = zCurrent + movementAllowance;
        zMin = zCurrent - movementAllowance;
        
        // +1 because its exclusive i think
        int xNew = UnityEngine.Random.Range(xMin, xMax);
        int zNew = UnityEngine.Random.Range(zMin, zMax);


        // now actually move the dam unit
        newId = aiInstance.AiCalculateNewSpaceId(Mathf.Clamp(xNew, 0, gridMax), Mathf.Clamp(zNew, 0,gridMax));

        gameObject.transform.position = gm.levelBuildManager.worldSpaceGrid[newId].transform.position;
        ocupiedSpace.GetComponent<GridSpace>().spaceMovable = true;
        ocupiedSpace = gm.levelBuildManager.worldSpaceGrid[newId];

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
        var instance = playerInstance.selection.GetComponent<Soldier>();
        instance.TakeDamage(firePower);
        actionEnd = true;

    }

    public override void AiShootConfirm(int playerTeamMax)
    {
        base.AiShootConfirm(playerTeamMax);
        int randomTarget = UnityEngine.Random.Range(0,playerTeamMax);
        Soldier instance = gm.redTeam[randomTarget].GetComponent<Soldier>();
        instance.TakeDamage(firePower);
    }


    public override IEnumerator WaitForActionEnd(int action)
    {

        // if player calls this 
        if (gm.gamePhase != GameManager.Phase.BattleAi)
        {
            // move action player
            if (action == 0)
            {
                while (actionEnd != true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (playerInstance.SelectionRaycast())
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
                        gm.PhaseLoop();
                    }
                    else
                    {
                        gm.gamePhase = GameManager.Phase.BattlePlayer;
                    }
                }
            }
            
            // shoot action player
            if (action == 1)
            {
                while (actionEnd != true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        playerInstance.SelectionRaycast();

                        if (playerInstance.selection.layer == LayerMask.NameToLayer("Ai Unit"))
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
                        gm.PhaseLoop();
                    }
                    else
                    {
                        gm.gamePhase = GameManager.Phase.BattlePlayer;
                    }
                }
            }
            
            
        }
        else
        {
            // if ai calls this
            if (action == 0)
            {
                
                //waits for ai to turn action end to true

                while (actionEnd != true)
                {

                    yield return null;
                }

                hasMoved = true;
            }
            
            if (action == 1)
            {
                
                //waits for ai to turn action end to true
                while (actionEnd != true)
                {

                    yield return null;
                }

                hasShot = true;
            }
        }
       

      

        actionEnd = false;
    }

    
    
    public override void Select()
    {
        base.Select();
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        CheckDeath();
    }

    public override void CheckDeath()
    {
        base.CheckDeath();
        if (CheckHp())
        {
            gm.redTeam.RemoveAt(unitId);
            if (gm.redTeam.Count == 0)
            {
                Debug.Log("Player is dead");
            }

            if (gm.blueTeam.Count == 0)
            {
                Debug.Log("Ai units Dead");
            }
            gameObject.SetActive(false);
        }

        
        
    }
}
