using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField UserNameField;
    public InputField PasswordField;

    public Button SubmitButton;

    public Text ErrorText;

    public Animator MessageAnimator;

    public void CallRegister()
    {
        StartCoroutine(IERegister());
    }

    private IEnumerator IERegister()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", UserNameField.text);
        form.AddField("password", PasswordField.text);
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("User created successfully");
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User creation failed Error#" + www.text);
            ErrorText.text = DBerrorhandeling.TranslateError(www.text);
            MessageAnimator.SetBool("PlayAnimation", true);
        }

    }

    public void VerifyInput()
    {
        SubmitButton.interactable = (UserNameField.text.Length >= 8 && PasswordField.text.Length >= 8);

    }

}
