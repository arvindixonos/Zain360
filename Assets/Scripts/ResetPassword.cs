using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Zain360 {

    public class ResetPassword : MonoBehaviour
    {
        public InputField username;
        
        public void ResetPass()
        {
            //if (username.Length < 5 || !username.Contains("@") || !username.Contains("."))
            //{
            //    print("Incorrect Username. Please try again");
            //}

            GameManager.Instance.ResetPassword(username.text);

            UIManager.Instance.SendMessageToCurrentPage("ShowErrorText");
        }
    }
};
