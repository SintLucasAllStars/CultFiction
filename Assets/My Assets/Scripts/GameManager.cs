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
        SelectingPlayerUnit = 2,
        SelectingAiUnit = 3,
        SpawningRedUnits = 4,
        PlayerUnitPointsEmpty = 5,
        AiUnitPointsEmpty = 6,
        SpawningBlueUnits = 7,
        BattleSideRed = 8,
        BattleSideBlue = 9,
        SwitchSide = 10,
        PrevSideBlue = 11,
        PrevSideRed = 12,
        BattleEnd = 13
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
            gamePhase = Phase.SelectingPlayerUnit;
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
        if (gamePhase == Phase.SelectingPlayerUnit)
        {
            Debug.Log("you can now spawn your units");
        }
    }



}
