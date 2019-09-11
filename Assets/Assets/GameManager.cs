using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //here keep track of the phases of battle
    public List<GameObject> redTeam;
    public List<GameObject> blueTeam;
    public enum Phase
    {
        PreBattle = 1,
        BattleSideRed = 2,
        SwitchSide = 3,
        BattleSideBlue = 4,
        BattleEnd = 5
    }
   public Phase gamePhase;

  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
