using System.Collections;
using System.Collections.Generic;
using LogIn;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomMenu : MonoBehaviour
{

    public Text MoneyText;
    public Text UsernameText;

    public void Start()
    {
        MoneyText.text ="MONEY: " + DBmanager.Money + "$";
        UsernameText.text = DBmanager.Username;
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
