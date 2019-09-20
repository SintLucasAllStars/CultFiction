using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad
{

    public static void Save() {     // Saving method

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate);
        SaveData saveData = new SaveData();

        saveData.lvl = GameManager.instance.lvl;
        saveData.scores = GameManager.instance.scores;

        bf.Serialize(file, saveData);
        Debug.Log("Saved File: " + file.Name);
        file.Close();
    }

    public static bool Load() {     // Loading method

        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);

            GameManager.instance.lvl = saveData.lvl;
            GameManager.instance.scores = saveData.scores;

            Debug.Log("Loaded File: " + file.Name);
            file.Close();
            return true;
        }
        else {
            return false;
        }

    }

}
