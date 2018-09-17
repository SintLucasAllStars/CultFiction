using System.Collections;
using System.Collections.Generic;
using LogIn;
using UnityEngine;

public class DBinventory : MonoBehaviour
{
    public GameObject Headband;
    public GameObject Glasses;
    public GameObject[] Jewelry;
    public GameObject Shoes;

    public void Start()
    {
        ActivateObjects();
    }



    public void SetHeadBand(bool value)
    {
        Headband.SetActive(value);
    }

    public void SetGlasses(bool value)
    {
        Glasses.SetActive(value);
    }

    public void SetJewelry(bool value)
    {
        foreach (var t in Jewelry)
        {
            t.SetActive(value);
        }
    }

    public void SetShoes(bool value)
    {
        Shoes.SetActive(value);
    }


    public void ActivateObjects()
    {
        if (DBmanager.HeadbandValue > 0)
        {
            Headband.SetActive(true);
        }

        if (DBmanager.GlassesValue > 0)
        {
            Glasses.SetActive(true);
        }

        if (DBmanager.JewelryValue > 0)
        {
            foreach (var t in Jewelry)
            {
                t.SetActive(true);
            }
        }

        if (DBmanager.ShoeValue > 0)
        {
            Shoes.SetActive(true);
        }
    }

  


}
