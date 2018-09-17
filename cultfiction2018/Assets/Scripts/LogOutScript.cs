using LogIn;
using UnityEngine;

public class LogOutScript  : MonoBehaviour
{

    private void OnApplicationQuit()
    {
        DBmanager.LogOut();
    }
}
