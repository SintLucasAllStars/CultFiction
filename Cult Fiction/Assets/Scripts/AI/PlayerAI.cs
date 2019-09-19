using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : BaseAI
{
    public enum UnitType {InfantryMan, Commando, Sniper}
    public UnitType unitType;

    private void Start()
    {
        SelectUnitType(1);
    }

    private void FixedUpdate()
    {
        SeeEnemy(9);

        if (baseTarget != null)
        {
            Aim(baseTarget);
            Debug.Log("Aiming");
        }
    }

    public void SelectUnitType(int id)
    {
        switch (unitType)
        {
            case UnitType.InfantryMan:
                CreateUnit(id, 10, 10, 3, 5, 10, 50);
                break;
            case UnitType.Commando:
                CreateUnit(id, 15, 15, 1, 5, 5, 50);
                    break;
            case UnitType.Sniper:
                CreateUnit(id, 5, 10, 5, 10, 15, 80);
                break;
        }
    }
}