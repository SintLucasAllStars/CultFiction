using System;
using System.Collections;
using System.Collections.Generic;
using LogIn;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPanel : MonoBehaviour
{
    public Text ProductText;

    public GameObject MyPanel;
    public DropDowns MyDropDowns;

    private DropDowns.DropDownOptions _currenOption;

    public Button ConfirmButton;

    public Animator MessageAnimator;

    public void SetPanelActive(string productText, DropDowns.DropDownOptions option)
    {
        int price = Price(option);

        if (price > DBmanager.Money)
        {
            ConfirmButton.interactable = false;
            StartCoroutine(IESetAnimation());

        }
        ProductText.text = "are you sure you want to buy " + productText;
        MyPanel.SetActive(true);
        _currenOption = option;
    }

    private IEnumerator IESetAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        MessageAnimator.SetBool("PlayAnimation", true);
    }

    private int Price(DropDowns.DropDownOptions option)
    {
        switch (option)
        {
            case DropDowns.DropDownOptions.Headband:
                return 250;

            case DropDowns.DropDownOptions.Glasses:
                return 500;

            case DropDowns.DropDownOptions.Jewerly:
                return 1000;

            case DropDowns.DropDownOptions.Shoes:
                return 5000;
            default:
                throw new ArgumentOutOfRangeException("option", option, null);
        }
    }


    public void SetPanelInactive()
    {
        MyPanel.SetActive(false);
    }


    public void Confirm()
    {

        DBmanager.Money -= Price(_currenOption);
        MyDropDowns.ActivateClothing(_currenOption);
        SetPanelInactive();
    }

    public void Deny()
    {
        MyDropDowns.DenyClothing(_currenOption);    
        SetPanelInactive();
    }


}
