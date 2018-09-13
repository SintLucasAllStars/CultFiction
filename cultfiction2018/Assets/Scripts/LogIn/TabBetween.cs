using UnityEngine;
using UnityEngine.UI;

namespace LogIn
{
    public class TabBetween : MonoBehaviour
    {
        public InputField UsernameField;
        public InputField PasswordField;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Tab)) return;
            if (UsernameField.isFocused)
            {
                PasswordField.ActivateInputField();
            }
            else if (PasswordField.isFocused)
            {
                UsernameField.ActivateInputField();
            }


        }
    }
}
