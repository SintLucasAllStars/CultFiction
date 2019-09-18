﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public int unitId;
    //public GameObject worldSpaceUnit;
    public GameObject unitMoveSet;
    public int unitCost;

    public enum unitStatus
    {
        inactive = 0,
        active = 1,
        selected = 2
    }

    public unitStatus unitState;
    public GameManager gm;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
        unitState = unitStatus.inactive;
        //worldSpaceUnit = this.gameObject;
        gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public virtual void Move()
    {
        
    }
    
    public virtual void Shoot(GameObject target)
    {
        
    }

    public virtual void Select()
    {
        unitState = Soldier.unitStatus.selected;
        unitMoveSet = gm.uiManager.unitMoveSet[unitId];
        unitMoveSet.SetActive(true);

    }

    void EndTurn()
    {
       
    }
}
