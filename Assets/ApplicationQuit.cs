using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationQuit : MonoBehaviour {

    public void OpenMainScene ()
    {
        SceneManager.LoadScene(0);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OpenMainScene();
        }
    }
}
