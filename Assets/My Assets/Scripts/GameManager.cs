using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //here keep track of the phases of battle
    public List<GameObject> redTeam;
    public List<GameObject> blueTeam;
    public List<GameObject> testPrefabUnits;

    // true is red false is blue

    public enum Phase
    {
        PreBattle = 1,
        BattleSideRed = 2,
        SwitchSide = 3,
        BattleSideBlue = 4,
        BattleEnd = 5
    }
    
    public enum ActiveTeam
    {
        RedTeam = 1,
        BlueTeam = 2,
        NoTeam = 3
    }

    public Phase gamePhase;
    public ActiveTeam activeTeam;


    // Start is called before the first frame update
    void Start()
    {
        gamePhase = Phase.SwitchSide;
        activeTeam = ActiveTeam.RedTeam;

    }

    // Update is called once per frame
    void Update()
    {
        if (gamePhase == Phase.SwitchSide)
        {
            Debug.Log("Switching side");
            if (activeTeam == ActiveTeam.RedTeam)
            {
                gamePhase = Phase.BattleSideRed;
            }
            else
            {
                gamePhase = Phase.BattleSideBlue;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnUnits();
        }
    }

    void SpawnUnits()
    {
        for (int i = 0; i < testPrefabUnits.Count; i++)
        {
            var unit = testPrefabUnits[i];
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
}
