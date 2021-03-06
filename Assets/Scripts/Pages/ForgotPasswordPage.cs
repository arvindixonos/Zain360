using UnityEngine;
using UnityEngine.UI;


namespace Zain360
{
    public class ForgotPasswordPage : Page
    {
        public InputField username;

        public GameObject errorText;

        public override void ShowPage()
        {
            base.ShowPage();

            username.text = "";
            errorText.gameObject.SetActive(false);
        }

        public void HomeClicked()
        {
            UIManager.Instance.ChangePage(ePages.LOGIN_SIGNUP_PAGE);
        }

        public void SignupSelected()
        {
            //AudioManager.Instance.PlaySound(eSoundList.SOUND_CLICK);

            //UIManager.Instance.ChangePage(ePages.SIGNUP_PAGE);
        }

        public void LoginSelected()
        {
            //AudioManager.Instance.PlaySound(eSoundList.SOUND_CLICK);

            //UIManager.Instance.ChangePage(ePages.LOGIN_PAGE);
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
        }

        public void ShowErrorText()
        {
            errorText.gameObject.SetActive(true);
        }
    }
}