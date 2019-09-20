using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AiPlayer : MonoBehaviour
{
    public List<GameObject> aiUnits;
    public GameManager gm;
    public GameObject selectedAiUnit;
    public Soldier aiUnitInstance;
    
    //!!! note ai doesnt take into account action points cause their is no direct need for it!!!
    // Start is called before the first frame update

    private void Start()
    {
        gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
    }

    public void TakeTurn()
    {
        aiUnits = gm.blueTeam;
        // il make stupid simple ai
        // it wil just choose random unit and move it forward and then shoot the closest player unit
        StartCoroutine(DoUnitActions());

        
    }

    void ChooseUnit(int unit)
    {
        selectedAiUnit = aiUnits[unit];
        aiUnitInstance = selectedAiUnit.GetComponent<Soldier>();
    }

    void MoveUnit()
    {
        int gridSize = gm.levelBuildManager.worldSpaceGrid.Count-1;
        int gridMin = 0;
        Vector3 unitPos;
        unitPos = aiUnitInstance.ocupiedSpace.transform.position;
        aiUnitInstance.Move();
        aiUnitInstance.AiMoveConfirm(gridSize,gridMin);
    }

    void ShootUnit()
    {
        aiUnitInstance.Shoot();
        aiUnitInstance.AiShootConfirm(gm.redTeam.Count);
    }

    public IEnumerator DoUnitActions()
    {
        for (int i = 0; i < aiUnits.Count; i++)
        {
            ChooseUnit(i);
            MoveUnit();
            ShootUnit(); 
            yield return new WaitForSeconds(1);
        }

        for (int i = 0; i < gm.redTeam.Count; i++)
        {
            Soldier playerUnit = gm.redTeam[i].GetComponent<Soldier>();
            

        }
        gm.gamePhase = GameManager.Phase.BattlePlayer;
        gm.PhaseLoop();
    }
    
    public  int AiCalculateNewSpaceId(int xAxis, int zAxis)
    {
        int id = 0;
        int x = xAxis;
        // because the grid is 20 * 20
        int z = zAxis * 20 ;

        id = x + z;
        Debug.Log(id);
        return id;
    }
    
    
}
