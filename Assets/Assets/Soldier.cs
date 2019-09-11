using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public enum unitStatus
    {
        inactive = 0,
        active = 1,
        selected = 2
    }

    [SerializeField] private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gamePhase == GameManager.Phase.BattleSideRed)
        {
            Debug.Log("RedSideTurn");
            gm.gamePhase = GameManager.Phase.SwitchSide;
        }

        if (gm.gamePhase == GameManager.Phase.BattleSideBlue)
        {
            Debug.Log("BlueSideTurn");
            gm.gamePhase = GameManager.Phase.SwitchSide;
        }
    }
}
