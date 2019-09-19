using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLouder : MonoBehaviour
{
    public void LoadScene(int sceneNumer)
    {
        SceneManager.LoadScene(sceneNumer);
    }
}
