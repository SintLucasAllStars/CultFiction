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
        BattleAi = 9,
        
        
        SwitchSide = 10,
        PrevSideBlue = 11,
        PrevSideRed = 12,
        
        BattleEnd = 13
    }
    

    public LevelBuildManager LevelBuildManager;
    public List<GameObject> redTeam;
    public List<GameObject> blueTeam;

    public GameObject selectedUnitToPlace;
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
        startingTeam = 1;
        LevelBuildManager = GameObject.Find("Game Managers and debug").GetComponent<LevelBuildManager>();
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
        LevelBuildManager.dGridScript.CreateDigitalGrid(20, 20);
        LevelBuildManager.CreateWorldSpaceGrid();

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
                redTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.active;
            }
            
            gamePhase = Phase.BattlePlayer;
            Debug.Log("player first move");
        }

        if (gamePhase == Phase.BattleAi)
        {
            for (int i = 0; i < redTeam.Count; i++)
            {
                redTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.active;
            }
            gamePhase = Phase.BattlePlayer;
        }
    }

    public void EndBattle()
    {
        //when 1 teams units are dead
    }

    
  
     



}
