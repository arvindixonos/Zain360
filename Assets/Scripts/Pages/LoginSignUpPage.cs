using UnityEngine;
using UnityEngine.UI;


namespace Zain360
{
    public class LoginSignUpPage : Page
    {
        public InputField firstnameField;
        public InputField lastnameField;
        public InputField su_usernameField;
        public InputField su_passwordField;


        public InputField lo_usernameField;
        public InputField lo_passwordField;


        public GameObject signupPanel;
        public GameObject loginPanel;
        public GameObject forgotPasswordPanel;
        public GameObject resetPasswordPanel;

        public Toggle loginToggle;
        public Toggle signupToggle;

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

        public void SelectLogin()
        {
            loginToggle.isOn = true;

            LoginSelected();
        }

        public void SelectSignup()
        {
            signupToggle.isOn = true;

            SignupSelected();
        }

        public void SignupClicked()
        {
            print("Signup Clicked");

            string firstName = firstnameField.text;
            string lastName = lastnameField.text;
            string username = su_usernameField.text;
            string password = su_passwordField.text;

            GameManager.Instance.Signup(firstName, lastName, username, password);
        }

        public void LoginClicked()
        {
            print("Login Clicked");

            UIManager.Instance.ChangePage(ePages.HOME_PAGE);
        }

        public void ForgotPasswordClicked()
        {
            print("Forgot Password Clicked");

            UIManager.Instance.ChangePage(ePages.FORGOT_RESET_PASSWORD_PAGE); 
        }

        public void OkClicked()
        {
            AudioManager.Instance.PlaySound(eSoundList.SOUND_CLICK);

            //string email = emailField.text.Trim();
            //string password = passwordField.text;
            //Debug.Log("Login");
            ////string email = "arun@gmail.com";
            ////string password = "123456";

            //if (email.Length < 6)
            //    return;

            //if (password.Length == 0)
            //    return;

            //UIManager.Instance.ShowLoadingPage();
            ////ProfileManager.Instance.SigninWithEmailAsync(email, password);
        }
    }
}