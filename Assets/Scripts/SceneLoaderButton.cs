using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderButton : GameButton
{
    public override void Press()
    {
        base.Press();
        SceneManager.LoadScene(1);
    }
}
