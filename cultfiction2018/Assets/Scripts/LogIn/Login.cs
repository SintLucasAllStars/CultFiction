using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LogIn
{
    public class Login : MonoBehaviour 
    {
        public InputField UserNameField;
        public InputField PasswordField;

        public Button SubmitButton;


        public Text ErrorText;

        public Animator MessageAnimator;



        public void CallLogin()
        {
            StartCoroutine(IELoginPlayer());
        }

        private IEnumerator IELoginPlayer()
        {
            WWWForm form = new WWWForm();
            form.AddField("username", UserNameField.text);
            form.AddField("password", PasswordField.text);
            WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
            yield return www;
            if (www.text[0] == '0')
            {
                DBmanager.Username = UserNameField.text;
                DBmanager.Score = int.Parse(www.text.Split('\t')[1]);
                DBmanager.HeadbandValue = int.Parse(www.text.Split('\t')[2]);
                DBmanager.GlassesValue = int.Parse(www.text.Split('\t')[3]);
                DBmanager.JewelryValue = int.Parse(www.text.Split('\t')[4]);
                DBmanager.ShoeValue = int.Parse(www.text.Split('\t')[5]);
                DBmanager.UnlockedHeadband =Convert.ToBoolean(int.Parse(www.text.Split('\t')[6]));
                DBmanager.UnlockedGlasses = Convert.ToBoolean(int.Parse(www.text.Split('\t')[7]));
                DBmanager.UnlockedJewelry = Convert.ToBoolean(int.Parse(www.text.Split('\t')[8]));
                DBmanager.UnlockedShoes = Convert.ToBoolean(int.Parse(www.text.Split('\t')[9]));
                DBmanager.Money = int.Parse(www.text.Split('\t')[10]);
                SceneManager.LoadScene("CustomizeScene");
            }
            else
            {
                Debug.Log("User login failed. Error#" + www.text);
                ErrorText.text = DBerrorhandeling.TranslateError(www.text);
                MessageAnimator.SetBool("PlayAnimation", true);
            }
        }
    
        public void VerifyInput()
        {
            SubmitButton.interactable = (UserNameField.text.Length >= 8 && PasswordField.text.Length >= 8);

        }
	
    }
}
