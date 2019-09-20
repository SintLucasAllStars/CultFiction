using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : BaseAI
{
    public enum UnitType {InfantryMan, Commando, Sniper}
    public UnitType unitType;

    public int baseGroupNumber;
    public bool isLeader;

    private AIManager aiManager;

    private readonly int detectLayerMask = 1 << 9;

    private void FixedUpdate()
    {
        if (behaviour == Behaviour.normal)
        {
            SeeEnemy(detectLayerMask);
        }

        if (behaviour == Behaviour.attack)
        {
            Aim(baseTarget);
            Shoot(baseTarget);

            Vector3 direction = transform.position - baseTarget.position;
            if (IsObscured(direction, detectLayerMask))
            {
                NoTarget();
            }
        }
    }

    public void SelectUnitType(int id, int groupNumber, AIManager ai, bool leader)
    {
        aiManager = ai;
        isLeader = leader;

        baseGroupNumber = groupNumber;

        switch (unitType)
        {
            case UnitType.InfantryMan:
                CreateUnit(id, 10, 10, 3, 5, 50, 50);
                break;
            case UnitType.Commando:
                CreateUnit(id, 15, 15, 1, 5, 70, 50);
                    break;
            case UnitType.Sniper:
                CreateUnit(id, 5, 10, 5, 10, 100, 80);
                break;
        }
    }

    public override void Death()
    {
        aiManager.PlayerDeath(this, isLeader);
        base.Death();
    }
}