using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject SyringePrefab;
    public HandMovement PlayerController;

    public void GetNewSyringe()
    {
        Instantiate(SyringePrefab, PlayerController.transform);
    }
}
