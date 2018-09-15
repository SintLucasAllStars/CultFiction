using System;
using System.Collections;
using System.Collections.Generic;
using LogIn;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DBSaver : MonoBehaviour {



    public void CallSaveData()
    {
        StartCoroutine(IESavePlayerData());
    }

    public void CallSaveData(string sceneName)
    {
        StartCoroutine(IESavePlayerData(sceneName));
    }

    private IEnumerator IESavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBmanager.Username);
        form.AddField("score", DBmanager.Score);
        form.AddField("Money", DBmanager.Money);
        form.AddField("HeadbandValue", DBmanager.HeadbandValue);
        form.AddField("GlassesValue", DBmanager.GlassesValue);
        form.AddField("JewelryValue", DBmanager.JewelryValue);
        form.AddField("ShoeValue", DBmanager.ShoeValue);
        form.AddField("UnlockedHeadband",Convert.ToInt32(DBmanager.UnlockedHeadband));
        form.AddField("UnlockedGlasses", Convert.ToInt32(DBmanager.UnlockedGlasses));
        form.AddField("UnlockedJewelry", Convert.ToInt32(DBmanager.UnlockedJewelry));
        form.AddField("UnlockedShoes", Convert.ToInt32(DBmanager.UnlockedShoes));

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Game Saved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }

        

    }

    private IEnumerator IESavePlayerData(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBmanager.Username);
        form.AddField("score", DBmanager.Score);
        form.AddField("Money", DBmanager.Money);
        form.AddField("HeadbandValue", DBmanager.HeadbandValue);
        form.AddField("GlassesValue", DBmanager.GlassesValue);
        form.AddField("JewelryValue", DBmanager.JewelryValue);
        form.AddField("ShoeValue", DBmanager.ShoeValue);
        form.AddField("UnlockedHeadband", Convert.ToInt32(DBmanager.UnlockedHeadband));
        form.AddField("UnlockedGlasses", Convert.ToInt32(DBmanager.UnlockedGlasses));
        form.AddField("UnlockedJewelry", Convert.ToInt32(DBmanager.UnlockedJewelry));
        form.AddField("UnlockedShoes", Convert.ToInt32(DBmanager.UnlockedShoes));

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Game Saved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }
        
        SceneManager.LoadScene(sceneName);

    }
}
