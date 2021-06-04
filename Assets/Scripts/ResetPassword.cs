using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Zain360 {

    public class ResetPassword : MonoBehaviour
    {
        public InputField username;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ResetPass()
        {
            //if (username.Length < 5 || !username.Contains("@") || !username.Contains("."))
            //{
            //    print("Incorrect Username. Please try again");
            //}
            print(username.text);
            GameManager.Instance.ResetPassword(username.text);
        }
    }
};
