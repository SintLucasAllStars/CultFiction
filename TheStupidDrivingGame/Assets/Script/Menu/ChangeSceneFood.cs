using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneFood : InCar
{
    public string sceneName;

    protected override void OnEat()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}