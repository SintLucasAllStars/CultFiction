using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : BaseAI
{
    private readonly int detectLayerMask = 1 << 8;

    private void Start()
    {
        CreateUnit(0, 5, 200, 2, 10, 100, 70);
    }

    private void FixedUpdate()
    {
        if (behaviour == Behaviour.normal)
        {
            SeeEnemy(detectLayerMask);
        }

        if (behaviour == Behaviour.attack)
        {
            if (baseTarget == null)
            {
                behaviour = Behaviour.normal;
                return;
            }
            Aim(baseTarget);
            Shoot(baseTarget);

            Vector3 direction = transform.position - baseTarget.position;
            if (IsObscured(direction, detectLayerMask))
            {
                NoTarget();
            }
        }
    }
}