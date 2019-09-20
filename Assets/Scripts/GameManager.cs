using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance = null;

    void Awake() {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [Header("Stats")]
    public int lvl;
    public int tries;
    public List<int> scores;
    public bool isComplete = false;

    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            SaveLoad.Load();
        }
    }

    public void LvComplete() {

        //Overwrite highscore ("low" score since it's golf)
        if (scores[SceneManager.GetActiveScene().buildIndex - 1] > tries || scores[SceneManager.GetActiveScene().buildIndex - 1] == 0)
        {
            scores[SceneManager.GetActiveScene().buildIndex - 1] = tries;
        }

        //Unlock new level if applies, return to menu
        if (SceneManager.GetActiveScene().buildIndex == lvl)
        {
            lvl++;
            tries = 0;
        }
        else
        {
            tries = 0;
        }
        isComplete = true;
        SaveLoad.Save();

    }

    public void PickLevel(int level) {      // Used by menu level buttons
        if (level <= instance.lvl)
        {
            SceneManager.LoadScene(level);
        }
    }

    public void ReturnHome() {
        SceneManager.LoadScene(0);
    }

}