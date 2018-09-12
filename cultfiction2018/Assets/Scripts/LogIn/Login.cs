using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LogIn
{
    public class Login : MonoBehaviour 
    {
        public InputField UserNameField;
        public InputField PasswordField;

        public Button SubmitButton;
    
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
            }
            else
            {
                Debug.Log("User login failed. Error#" + www.text);
            }
        }
    
        public void VerifyInput()
        {
            SubmitButton.interactable = (UserNameField.text.Length >= 8 && PasswordField.text.Length >= 8);

        }
	
    }
}
