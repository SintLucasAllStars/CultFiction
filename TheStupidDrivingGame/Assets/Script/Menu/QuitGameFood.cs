using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameFood : InCar
{
    protected override void OnEat()
    {
        Application.Quit();
    }
}