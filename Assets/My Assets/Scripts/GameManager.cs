using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> unitDataBase;
    //here are the phases of battle
    public enum Phase
    {
        PreBattle = 1,
        SelectingRedUnit = 2,
        SelectingBlueUnit = 3,
        SpawningRedUnits = 4,
        SpawningBlueUnits = 5,
        BattleSideRed = 6,
        SwitchSide = 7,
        PrevSideBlue = 8,
        PrevSideRed = 9,
        BattleSideBlue = 10,
        BattleEnd = 11
    }
    
    public enum ActiveTeam
    {
        RedTeam = 1,
        BlueTeam = 2,
        NoTeam = 3,
        AiTeam = 4
    }

    public LevelBuildManager LevelBuildManager; 
    public List<GameObject> redTeam;
    public List<GameObject> blueTeam;
    /*public List<GameObject> testPrefabUnitsRed;
    public List<GameObject> testPrefabUnitsBlue;*/

    private int startingTeam;

    // true is red false is blue
    
    public Phase gamePhase;
    public ActiveTeam activeTeam;


    private void Awake()
    {
        activeTeam = ActiveTeam.NoTeam;
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
            SwitchToRedTeam();
            gamePhase = Phase.SelectingRedUnit;
            PhaseLoop();

        }
        else
        {
            SwitchToBlueTeam();
            gamePhase = Phase.SpawningBlueUnits;
        }
        
    }

    void SwitchToRedTeam()
    {
        for (int i = 0; i < redTeam.Count; i++)
        {
            activeTeam = ActiveTeam.RedTeam;
            //redTeam[i].GetComponent<RedSoldier>().unitState = Soldier.unitStatus.active;
        }
    }
    
    void SwitchToBlueTeam()
    {
        for (int i = 0; i < redTeam.Count; i++)
        {
            activeTeam = ActiveTeam.BlueTeam;
            //blueTeam[i].GetComponent<BlueSoldier>().unitState = Soldier.unitStatus.active;
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

    

    public void ChangePhase()
    {
        if (gamePhase == Phase.PrevSideRed)
        {
            
            gamePhase = Phase.BattleSideBlue;
        }
        else if (gamePhase == Phase.PrevSideBlue)
        {
            gamePhase = Phase.BattleSideRed;
        }
    }

    public void PhaseLoop()
    {
        if (gamePhase == Phase.SelectingRedUnit)
        {
            Debug.Log("you can now spawn your units");
        }
    }



}
