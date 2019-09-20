using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public AiPlayer aiInstance;

    public Player playerInstance;
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

    public void PhaseLoop()
    {
        // player loop
        if (gamePhase == Phase.SelectingPlayerUnit)
        {
            TextMeshProUGUI statusText = uiManager.statusDisplay;
            statusText.text = "Status:\nselect and place your units";

            uiManager.unitSelectionUi.SetActive(true);
        }

        // Ai loop
        if (gamePhase == Phase.SelectingAiUnit)
        {
            Debug.Log("Ai is now selecting units");
            gamePhase = Phase.SpawningAiUnits;
        }

        if (gamePhase == Phase.SpawningAiUnits)
        {
            uiManager.unitSelectionUi.SetActive(false);
            GameObject unit;
            Soldier soldierInstance;
            Debug.Log("Ai is spawning units");

            // I only have 1 unit type to spawn the stormtroopers
            for (int i = 0; i < aiPattern.Count; i++)
            {
                Vector3 spawnPos = aiPattern[i];
                int spawnPosx = Mathf.FloorToInt(spawnPos.x);
                int spawnPosz = Mathf.FloorToInt(spawnPos.z);

                unit = Instantiate(unitDataBase[1], new Vector3(spawnPos.x, spawnPos.y + 0.5f, spawnPos.z), unitDataBase[1].transform.rotation);
                soldierInstance = unit.GetComponent<Soldier>();
                blueTeam.Add(unit);
                // -1 because count return 1 too much
                int worldGridId = aiInstance.AiCalculateNewSpaceId(spawnPosx, spawnPosz);
                soldierInstance.ocupiedSpace = levelBuildManager.worldSpaceGrid[worldGridId];
                //unitid i think does not get used
                soldierInstance.unitId = i;

                // activate player stormtrooper moveset ui

                uiManager.UpdateStatus("select unit to use their actions");
            }

            // player begins
            for (int i = 0; i < redTeam.Count; i++)
            {
                redTeam[i].GetComponent<Soldier>().unitState = Soldier.unitStatus.Active;
            }

            gamePhase = Phase.BattlePlayer;
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

            uiManager.UpdateStatus("its your turn select a unit to use");
        }

        if (gamePhase == Phase.BattleAi)
        {
            uiManager.UpdateStatus("ai is taking his turn");
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
    public bool CheckTeamActionPoints()
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

    public void ResetUnitsId()
    {
        for (int i = 0; i < redTeam.Count; i++)
        {
            redTeam[i].GetComponent<Soldier>().unitId = i;
        }

        for (int i = 0; i < blueTeam.Count; i++)
        {
            blueTeam[i].GetComponent<Soldier>().unitId = i;
        }
    }
}
