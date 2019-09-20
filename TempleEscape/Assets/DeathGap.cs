using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGap : MonoBehaviour, ITriggerable
{

    public GameObject DeathGapStones;

    public void Triggerd()
    {
        DeathGapStones.SetActive(false);
    }

    public void Reset()
    {
        DeathGapStones.SetActive(true);
    }
}
