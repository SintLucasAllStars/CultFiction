using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> unitDataBase;
    public List<Vector3> aiPattern;
    //here are the phases of battle
    public enum Phase
    {
        PreBattle = 1,
        SelectingPlayerUnit = 2,
        SelectingAiUnit = 3,
        SpawningPlayerUnits = 4,
        PlayerUnitPointsEmpty = 5,
        AiUnitPointsEmpty = 6,
        SpawningAiUnits = 7,
        BattlePlayer = 8,
        SelectPlayerUnitAction = 9,
        SelectAiUnitAction = 10, 
        BattleAi = 11,
        
        BattleEnd = 13
    }
    

    public LevelBuildManager levelBuildManager;
    public UiButtonManager uiManager;
    public List<GameObject> redTeam;
    public List<GameObject> blueTeam;

    public GameObject selectedUnitToPlace;
    public GameObject selectedActiveUnit;
    /*public List<GameObject> testPrefabUnitsRed;
    public List<GameObject> testPrefabUnitsBlue;*/

    private int startingTeam;

    // true is red false is blue

    public Phase gamePhase;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
        // red team starts
        uiManager = GameObject.Find("Game Managers and debug").GetComponent<UiButtonManager>();
        levelBuildManager = GameObject.Find("Game Managers and debug").GetComponent<LevelBuildManager>();
        startingTeam = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Level Setup Later put in function
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetupLevel();
        }
    }

    void SetupLevel()
    {
        gamePhase = Phase.PreBattle;
        levelBuildManager.dGridScript.CreateDigitalGrid(20, 20);
        levelBuildManager.CreateWorldSpaceGrid();

        if (startingTeam == 1)
        {
            gamePhase = Phase.SelectingPlayerUnit;
            PhaseLoop();
        }
        else
        {
            // havent started with ai yet it wil probably brake the turn order
            gamePhase = Phase.SpawningAiUnits;
        }
    }

   



    void AssignUnitToTeam()
    {
        /*for (int i = 0; i < testPrefabUnitsRed.Count; i++)
        {
         

            if (spawnedUnit.CompareTag("Red Team"))
            {
                redTeam.Add(spawnedUnit);
            }
            else
            {
                blueTeam.Add(spawnedUnit);
            }
        }*/
    }

    /*public void ChangePhase()
    {
        if (gamePhase == Phase.PrevSideRed)
        {
            gamePhase = Phase.BattleSideBlue;
        }
        else if (gamePhase == Phase.PrevSideBlue)
        {
            gamePhase = Phase.BattleSideRed;
        }
    }*/

    public void PhaseLoop()
    {
        // player loop
        if (gamePhase == Phase.SelectingPlayerUnit)
        {
            Debug.Log("you can now select and place your units");
            Debug.Log("Game is waiting for player input");
        }

        if (gamePhase == Phase.BattlePlayer)
        {
            gamePhase = Phase.BattleAi;
        }

        // Ai loop
        if (gamePhase == Phase.SelectingAiUnit)
        {
            Debug.Log("Ai is now selecting units");
            gamePhase = Phase.SpawningAiUnits;
        }

        if (gamePhase == Phase.SpawningAiUnits)
        {
            GameObject unit;
            Debug.Log("Ai is spawning units");
            for (int i = 0; i < aiPattern.Count; i++)
            {
                unit = Instantiate(unitDataBase[1], aiPattern[i], Quaternion.identity);
                blueTeam.Add(unit);
            }

            for (int i = 0; i < redTeam.Count; i++)
            {
                redTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.Active;
            }
            
            gamePhase = Phase.BattlePlayer;
            Debug.Log("player first move");
        }

        if (gamePhase == Phase.BattleAi)
        {
            
            // here initiate ai loop
            for (int i = 0; i < redTeam.Count; i++)
            {
                redTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.Active;
            }
            
            for (int i = 0; i < blueTeam.Count; i++)
            {
                blueTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.Inactive;
            }
            
            

            gamePhase = Phase.BattlePlayer;
        }
    }


    //check after every action so you know if all units have done their actions.
    public bool CheckTeam()
    {
        int amountOfUnits = redTeam.Count;
        int amount = 0;

        foreach (var soldierInstance in redTeam)
        {
            Soldier instance = soldierInstance.GetComponent<Soldier>();
           
            // mayby change to check unitstate later
            if (instance.CheckActionPoints() != true)
            {
                amount = amount + 1;
                Debug.Log("Selected unit cant do anymore action");
            }
        }

        if (amount == amountOfUnits)
        {
           
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void EndBattle()
    {
        //when 1 teams units are dead
    }

    
  
     



}
