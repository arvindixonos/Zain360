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


        public TabPanel signupPanel;
        public TabPanel loginPanel;
        public GameObject forgotPasswordPanel;
        public GameObject resetPasswordPanel;

        public Toggle loginToggle;
        public Toggle signupToggle;

        public Text loginErrorText;
        public Text signupErrorText;

        public override void Awake()
        {
            base.Awake();

            LoginSelected();
        }

        public override void ShowPage()
        {
            base.ShowPage();

            ClearLoginFields();
            ClearSignupFields();
        }

        public void ClearSignupFields()
        {
            su_usernameField.text = "";
            su_passwordField.text = "";
            firstnameField.text = "";
            lastnameField.text = "";
        }

        public void SignupSelected()
        {
            ClearSignupFields();

            signupPanel.gameObject.SetActive(true);
            signupPanel.PanelSelected();

            loginPanel.gameObject.SetActive(false);
            loginPanel.PanelDeselected();

            //AudioManager.Instance.PlaySound(eSoundList.SOUND_CLICK);

            //UIManager.Instance.ChangePage(ePages.SIGNUP_PAGE);
        }

        public void ClearLoginFields()
        {
            lo_usernameField.text = "";
            lo_passwordField.text = "";
        }

        public void LoginSelected()
        {
            //AudioManager.Instance.PlaySound(eSoundList.SOUND_CLICK);

            //UIManager.Instance.ChangePage(ePages.LOGIN_PAGE);

            ClearLoginFields();

            signupPanel.gameObject.SetActive(false);
            signupPanel.PanelDeselected();

            loginPanel.gameObject.SetActive(true);
            loginPanel.PanelSelected();
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

            HideSignupErrorText();

            string firstName = firstnameField.text.Trim();
            string lastName = lastnameField.text.Trim();
            string username = su_usernameField.text.Trim();
            string password = su_passwordField.text.Trim();

            if (username.Length < 5 || !username.Contains("@") || !username.Contains("."))
            {
                ShowSignupErrorText("Incorrect Username.");
            }
            else if (password.Length == 0)
            {
                ShowSignupErrorText("Password cannot be empty.");
            }
            else if (firstName.Length == 0)
            {
                ShowSignupErrorText("First Name cannot be empty.");
            }
            else if (firstName.Length == 0)
            {
                ShowSignupErrorText("Last Name cannot be empty.");
            }
            else
            {
                GameManager.Instance.Signup(firstName, lastName, username, password);
            }
        }

        private void ShowLoginErrorText(string text)
        {
            loginErrorText.text = text;
            loginErrorText.gameObject.SetActive(true);
        }

        private void HideLoginErrorText()
        {
            loginErrorText.gameObject.SetActive(false);
        }

        private void ShowSignupErrorText(string text)
        {
            signupErrorText.text = text;
            signupErrorText.gameObject.SetActive(true);
        }

        private void HideSignupErrorText()
        {
            signupErrorText.gameObject.SetActive(false);
        }

        public void LoginClicked()
        {
            print("Login Clicked");

            HideLoginErrorText();

            string username = lo_usernameField.text.Trim();
            string password = lo_passwordField.text.Trim();
            
            if(username.Length < 5 || !username.Contains("@") || !username.Contains("."))
            {
                ShowLoginErrorText("Incorrect Username. Please try again");
            }
            else if(password.Length == 0)
            {
                ShowLoginErrorText("Password cannot be empty");
            }
            else
            {
                GameManager.Instance.Login(username, password);
            }
        }

        public void ForgotPasswordClicked()
        {
            print("Forgot Password Clicked");

            UIManager.Instance.ChangePage(ePages.FORGOT_RESET_PASSWORD_PAGE); 
        }

        public override void Tabbed(bool shift = false)
        {
            signupPanel.Tabbed(shift);
            loginPanel.Tabbed(shift);
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