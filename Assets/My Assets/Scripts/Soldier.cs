using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public Player playerInstance;
    public AiPlayer aiInstance;
    //type of unit
    public int unitId;
    //list id i dont use it i think
    public int worldSpaceGridId;
    //public GameObject worldSpaceUnit;
    public GameObject unitMoveSet;
    public int unitCost;
    public int movementAllowance;
    
    public bool hasMoved;
    public bool hasShot;
    public bool actionEnd;
    public GameObject ocupiedSpace;
    public int hp;
    public int firePower;
    public GameObject unitShader;

    public enum unitStatus
    {
        Inactive = 0,
        Active = 1,
        Selected = 2,
        DoingAction = 3
    }

    public unitStatus unitState;
    public GameManager gm;
    // Start is called before the first frame update


    public virtual void Start()
    {
        //worldSpaceUnit = this.gameObject;
        playerInstance = GameObject.Find("Player Object").GetComponent<Player>();
        aiInstance = GameObject.Find("Ai Object").GetComponent<AiPlayer>();
        gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
        
        unitState = unitStatus.Inactive;
        hasMoved = false;
        hasShot = false;
        actionEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public virtual void Move()
    {
        
    }
    
    public virtual void MoveConfirm()
    {
        
    }

    public virtual void AiMoveConfirm(int gridMax, int gridMinimum)
    {
        
    }

    // just do this for 2D grid for now
    

    public virtual void ShootConfirm()
    {
        
    }

    public virtual void AiShootConfirm(int playerTeamMax)
    {
        
    }


    public virtual void Shoot()
    {
        
    }

    public virtual void Select()
    {
        unitState = Soldier.unitStatus.Selected;
        unitMoveSet = gm.uiManager.unitMoveSet[unitId];
        unitMoveSet.SetActive(true);
        gm.gamePhase = GameManager.Phase.SelectPlayerUnitAction;

    }

    void EndTurn()
    {
       
    }
    
    public virtual IEnumerator WaitForActionEnd(int action)
    {
        return null;
    }

    public bool CheckActionPoints()
    {
        if (hasMoved == false || hasShot == false)
        {
            return true;
        }
        return false;
    }

    public virtual void TakeDamage(int dmg)
    {
        hp = hp - dmg;
        
    }

    public void ResetActionPoints()
    {
        hasMoved = false;
        hasShot = false;
    }

    public bool CheckHp()
    {
        if (hp <= 0)
        {
            return true;
        }

        return false;
    }

    public virtual void CheckDeath()
    {
        
    }
}
