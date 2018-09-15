using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPanel : MonoBehaviour
{
    public Text ProductText;

    public GameObject MyPanel;
    public DropDowns MyDropDowns;

    private DropDowns.DropDownOptions _currenOption;

    public void SetPanelActive(string productText, DropDowns.DropDownOptions option)
    {
        ProductText.text = "are you sure you want to buy " + productText;
        MyPanel.SetActive(true);
        _currenOption = option;
    }

    public void SetPanelInactive()
    {
        MyPanel.SetActive(false);
    }


    public void Confirm()
    {
        MyDropDowns.ActivateClothing(_currenOption);
        SetPanelInactive();
    }

    public void Deny()
    {
        SetPanelInactive();
    }


}
