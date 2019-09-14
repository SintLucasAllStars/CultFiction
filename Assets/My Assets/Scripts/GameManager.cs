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
        SpawningRedUnits = 2,
        SpawningBlueUnits = 3,
        BattleSideRed = 3,
        SwitchSide = 4,
        PrevSideBlue = 5,
        PrevSideRed = 6,
        BattleSideBlue = 7,
        BattleEnd = 8
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
    public List<GameObject> testPrefabUnitsRed;
    public List<GameObject> testPrefabUnitsBlue;

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
        

    }

    // Update is called once per frame
    void Update()
    {
        // Level Setup Later put in function
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //SpawnUnits();
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
            gamePhase = Phase.SpawningRedUnits;
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

    void SpawnUnits()
    {
        for (int i = 0; i < testPrefabUnitsRed.Count; i++)
        {
            var unit = testPrefabUnitsRed[i];
            var spawnedUnit = Instantiate(unit, unit.transform.position, Quaternion.identity);

            if (spawnedUnit.CompareTag("Red Team"))
            {
                redTeam.Add(spawnedUnit);
            }
            else
            {
                blueTeam.Add(spawnedUnit);
            }
        }
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
        if (gamePhase == Phase.SpawningRedUnits)
        {
            Debug.Log("you can now spawn your units");
        }
    }



}
