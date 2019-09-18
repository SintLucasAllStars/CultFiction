using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public GameObject worldSpaceUnit;
    public int unitCost;

    public enum unitStatus
    {
        inactive = 0,
        active = 1,
        selected = 2
    }

    public unitStatus unitState;
    public GameManager gm;
    // Start is called before the first frame update
    public virtual void Start()
    {
        unitState = unitStatus.inactive;
        worldSpaceUnit = this.gameObject;
        gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public virtual void Move()
    {
        
    }
    
    public virtual void Shoot(GameObject target)
    {
        
    }

    void EndTurn()
    {
       
    }
}
