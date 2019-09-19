using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _isOpened;

    public bool IsOpened => _isOpened;

    public void ToggleOpen()
    {
        if (ObjectManager.Instance.GetChairs().Length == 0)
        {
            ErrorMessage.Instance.AnnounceError("Please place a table for customers to sit on");
            return;
        }

        if (ObjectManager.Instance.GetMachines().Length == 0)
        {
            ErrorMessage.Instance.AnnounceError("Please place a machine for making food");
            return;
        }

        _isOpened = !_isOpened;
    }
}
