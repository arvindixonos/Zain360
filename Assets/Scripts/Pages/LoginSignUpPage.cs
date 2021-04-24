using UnityEngine;
using UnityEngine.UI;


namespace Zain360
{
    public class LoginSignUpPage : Page
    {
        public InputField emailField;
        public InputField passwordField;

        public GameObject signupPanel;
        public GameObject loginPanel;
        public GameObject forgotPasswordPanel;
        public GameObject resetPasswordPanel;

        public override void ShowPage()
        {
            base.ShowPage();
        }

        public void SignupSelected()
        {
            signupPanel.SetActive(true);
            loginPanel.SetActive(false);

            //AudioManager.Instance.PlaySound(eSoundList.SOUND_CLICK);

            //UIManager.Instance.ChangePage(ePages.SIGNUP_PAGE);
        }

        public void LoginSelected()
        {
            //AudioManager.Instance.PlaySound(eSoundList.SOUND_CLICK);

            //UIManager.Instance.ChangePage(ePages.LOGIN_PAGE);

            signupPanel.SetActive(false);
            loginPanel.SetActive(true);
        }

        public void SignupClicked()
        {
            print("Signup Clicked");
        }

        public void LoginClicked()
        {
            print("Login Clicked");
        }

        public void ForgotPasswordClicked()
        {
            print("Forgot Password Clicked");

            UIManager.Instance.ChangePage(ePages.FORGOT_RESET_PASSWORD_PAGE); 
        }

        public void OkClicked()
        {
            AudioManager.Instance.PlaySound(eSoundList.SOUND_CLICK);

            string email = emailField.text.Trim();
            string password = passwordField.text;
            Debug.Log("Login");
            //string email = "arun@gmail.com";
            //string password = "123456";

            if (email.Length < 6)
                return;

            if (password.Length == 0)
                return;

            UIManager.Instance.ShowLoadingPage();
            //ProfileManager.Instance.SigninWithEmailAsync(email, password);
        }
    }
}