using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public UiManager uiManager;
    public List<GameObject> redTeam;
    public List<GameObject> blueTeam;

    public GameObject selectedUnitToPlace;
    public GameObject selectedActiveUnit;

    public GameObject aiObject;
    public AiPlayer aiInstance;
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
        uiManager = GameObject.Find("Game Managers and debug").GetComponent<UiManager>();
        levelBuildManager = GameObject.Find("Game Managers and debug").GetComponent<LevelBuildManager>();
        startingTeam = 1;
        aiObject = GameObject.Find("Ai Object");
        aiInstance = aiObject.GetComponent<AiPlayer>();

    }

    // Update is called once per frame
    void Update()
    {
        // Level Setup Later put in function
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetupLevel();
        }

        if (Input.GetKeyDown((KeyCode.E)))
        {
            blueTeam.RemoveAt(blueTeam.Count - 1);
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

        

        // Ai loop
        if (gamePhase == Phase.SelectingAiUnit)
        {
            Debug.Log("Ai is now selecting units");
            gamePhase = Phase.SpawningAiUnits;
        }

        if (gamePhase == Phase.SpawningAiUnits)
        {
            GameObject unit;
            Soldier soldierInstance;
            Debug.Log("Ai is spawning units");
            for (int i = 0; i < aiPattern.Count; i++)
            {
                Vector3 spawnPos = aiPattern[i];
                int spawnPosx = Mathf.FloorToInt(spawnPos.x);
                int spawnPosz = Mathf.FloorToInt(spawnPos.z);
                
                unit = Instantiate(unitDataBase[1], new Vector3(spawnPos.x,spawnPos.y + 0.5f,spawnPos.z), unitDataBase[1].transform.rotation);
                soldierInstance = unit.GetComponent<Soldier>();
                blueTeam.Add(unit);
                // -1 because count return 1 too much
                int worldGridId = aiInstance.AiCalculateNewSpaceId(spawnPosx, spawnPosz);
                soldierInstance.ocupiedSpace = levelBuildManager.worldSpaceGrid[worldGridId];
                soldierInstance.unitId = i;
            }

            // player begins
            for (int i = 0; i < redTeam.Count; i++)
            {
                redTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.Active;
            }
            
            gamePhase = Phase.BattlePlayer;
            Debug.Log("player first move");
        }
        
        if (gamePhase == Phase.BattlePlayer)
        {
            for (int i = 0; i < redTeam.Count; i++)
            {
                Soldier playerUnit = redTeam[i].GetComponent<Soldier>();
                playerUnit.unitState = Soldier.unitStatus.Active;
                playerUnit.ResetActionPoints();
            }
            
            for (int i = 0; i < blueTeam.Count; i++)
            {
                blueTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.Inactive;
            }
        }
        
        if (gamePhase == Phase.BattleAi)
        {
            
            // here initiate ai loop
            for (int i = 0; i < redTeam.Count; i++)
            {
                redTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.Inactive;
            }
            
            for (int i = 0; i < blueTeam.Count; i++)
            {
                blueTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.Active;
            }
            
            aiInstance.TakeTurn();
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

    public void EndBattle(bool winner)
    {
        //when 1 teams units are dead
        gamePhase = Phase.BattleEnd;
        if (winner != true)
        {
            uiManager.generalUi[0].SetActive(true);
        }
    }

    // restart button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }







}
