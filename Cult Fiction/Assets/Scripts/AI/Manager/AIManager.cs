using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<PlayerAI> playerAI;
    private PlayerAI selectedUnit;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MoveSelectedUnit(playerAI[0]);
        }
    }

    private void MoveSelectedUnit(PlayerAI playerAI)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            playerAI.MoveTo(hit.point);
        }
    }
}