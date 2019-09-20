using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AiPlayer : MonoBehaviour
{
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
        // il make stupid simple ai
        // it wil just choose random unit and move it forward and then shoot the closest player unit
        StartCoroutine(DoUnitActions());
    }

    void ChooseUnit(int unit)
    {
        selectedAiUnit = gm.blueTeam[unit];
        aiUnitInstance = selectedAiUnit.GetComponent<Soldier>();
    }

    void MoveUnit()
    {
        int gridSize = gm.levelBuildManager.worldSpaceGrid.Count - 1;
        int gridMin = 0;
        Vector3 unitPos;
        unitPos = aiUnitInstance.ocupiedSpace.transform.position;
        aiUnitInstance.Move();
        aiUnitInstance.AiMoveConfirm(gridSize, gridMin);
    }

    void ShootUnit()
    {
        aiUnitInstance.Shoot();
        aiUnitInstance.AiShootConfirm(gm.redTeam.Count);
    }

    public IEnumerator DoUnitActions()
    {
        for (int i = 0; i < gm.blueTeam.Count; i++)
        {
            ChooseUnit(i);
            MoveUnit();
            ShootUnit();
            yield return new WaitForSeconds(0.5f);
        }

        gm.uiManager.UpdateStatus("ai is done");
        gm.gamePhase = GameManager.Phase.BattlePlayer;
        gm.PhaseLoop();
    }

    // for moving the ai units
    public int AiCalculateNewSpaceId(int xAxis, int zAxis)
    {
        int id = 0;
        int x = xAxis;
        // because the grid is 20 * 20
        int z = zAxis * 20;

        id = x + z;
        return id;
    }
}
