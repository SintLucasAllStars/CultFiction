using System.Collections;
using System.Collections.Generic;
using LogIn;
using UnityEngine;

public class LogOutScript : MonoBehaviour {


    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        DBmanager.LogOut();
    }
}
