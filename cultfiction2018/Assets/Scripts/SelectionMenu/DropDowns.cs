using System;
using System.Collections;
using System.Collections.Generic;
using LogIn;
using UnityEngine;
using UnityEngine.UI;

public class DropDowns : MonoBehaviour
{
  
    public CustomDropdown HeadbandCustom;
    public CustomDropdown GlassesCustom;
    public CustomDropdown JewelryCustom;
    public CustomDropdown ShoeCustom;

    public DropdownItem HeadbandItemOne;
    public DropdownItem HeadbandItemTwo;
    public DropdownItem GlassesItemOne;
    public DropdownItem GlassesItemTwo;
    public DropdownItem JewelryItemOne;
    public DropdownItem JewelryItemTwo;
    public DropdownItem ShoeItemOne;
    public DropdownItem ShoeItemTwo;

    public DBinventory MyDBinventory;
    public ConfirmationPanel MyConfirmationPanel;

    public enum DropDownOptions
    {
        Headband,
        Glasses,
        Jewerly,
        Shoes
    }

    public void Start()
    {
        ConfigureNames();
    }


    public void ConfigureNames()
    {

        HeadbandCustom.selectedText.text = DBmanager.HeadbandValue > 0 ? HeadbandItemTwo.itemText : HeadbandItemOne.itemText;

        GlassesCustom.selectedText.text = DBmanager.GlassesValue > 0 ? GlassesItemTwo.itemText : GlassesItemOne.itemText;
        JewelryCustom.selectedText.text = DBmanager.JewelryValue > 0 ? JewelryItemTwo.itemText : JewelryItemOne.itemText;
        ShoeCustom.selectedText.text = DBmanager.ShoeValue > 0 ? ShoeItemTwo.itemText : ShoeItemOne.itemText;
    }

    public void FirstHeadBandOptionPressed()
    {
        MyDBinventory.SetHeadBand(false);
        DBmanager.HeadbandValue = 0;
    }

    public void SecondHeadBandOptionPressed()
    {
        if (!DBmanager.UnlockedHeadband)
        {
            MyConfirmationPanel.SetPanelActive(HeadbandItemTwo.itemText,DropDownOptions.Headband);
        }
        else
        {
            ActivateClothing(DropDownOptions.Headband);
        }
     
    }

    public void FirstGlassesOptionPressed()
    {
        MyDBinventory.SetGlasses(false);
        DBmanager.GlassesValue = 0;
    }

    public void SecondGlassesOptionPressed()
    {
        if (!DBmanager.UnlockedGlasses)
        {
            MyConfirmationPanel.SetPanelActive(GlassesItemTwo.itemText,DropDownOptions.Glasses);
        }
        else
        {
            ActivateClothing(DropDownOptions.Glasses);
        }
    }

    public void FirstJewelryOptionPressed()
    {
        MyDBinventory.SetJewelry(false);
        DBmanager.JewelryValue = 0;
    }

    public void SecondJewelryOptionPressed()
    {
        if (!DBmanager.UnlockedJewelry)
        {
            MyConfirmationPanel.SetPanelActive(JewelryItemTwo.itemText, DropDownOptions.Jewerly);
        }
        else
        {
            ActivateClothing(DropDownOptions.Jewerly);
        }
    }

    public void FirstShoeOptionPressed()
    {
        MyDBinventory.SetShoes(false);
        DBmanager.ShoeValue = 0;
    }

    public void SecondShoeOptionPressed()
    {
        if (!DBmanager.UnlockedShoes)
        {
            MyConfirmationPanel.SetPanelActive(ShoeItemTwo.itemText, DropDownOptions.Shoes);
        }
        else
        {
            ActivateClothing(DropDownOptions.Shoes);
        }
    }

    public void DenyClothing(DropDownOptions option)
    {
      
        switch (option)
        {
            case DropDownOptions.Headband:
                HeadbandCustom.selectedText.text = HeadbandItemOne.itemText;
                break;
            case DropDownOptions.Glasses:

               GlassesCustom.selectedText.text = GlassesItemOne.itemText;
                break;
            case DropDownOptions.Jewerly:
                JewelryCustom.selectedText.text = JewelryItemOne.itemText;
                break;
            case DropDownOptions.Shoes:
                ShoeCustom.selectedText.text = ShoeItemOne.itemText;
                break;
            default:
                throw new ArgumentOutOfRangeException("option", option, null);
        }
    }


    //todo room for improvement
    public void ActivateClothing(DropDownOptions option)
    {
        switch (option)
        {
               
            case DropDownOptions.Headband:
                MyDBinventory.SetHeadBand(true);
                DBmanager.HeadbandValue = 1;
                DBmanager.UnlockedHeadband = true;
                break;
            case DropDownOptions.Glasses:
                MyDBinventory.SetGlasses(true);
                DBmanager.GlassesValue = 1;
                DBmanager.UnlockedGlasses = true;
                break;
            case DropDownOptions.Jewerly:
                MyDBinventory.SetJewelry(true);
                DBmanager.JewelryValue = 1;
                DBmanager.UnlockedJewelry = true;
                break;
            case DropDownOptions.Shoes:
                MyDBinventory.SetShoes(true);
                DBmanager.ShoeValue = 1;
                DBmanager.UnlockedShoes = true;
                break;
            default:
                throw new ArgumentOutOfRangeException("option", option, null);
        }
    }



   



}
