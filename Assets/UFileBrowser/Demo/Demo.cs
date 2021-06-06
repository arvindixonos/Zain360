using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace uFileBrowser
{

    public class Demo : UnityEngine.MonoBehaviour
    {

        public UnityEngine.UI.InputField IF;
        Texture2D texNotFound;
        private string filelocationinput = "";
        //public RawImage testImage;
        public byte[] tempBlob;
        public static Demo Instance;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void OpenFile(string path)
        {
            filelocationinput = path;
            StartCoroutine(LoadTexture());
            print(tempBlob);
        }


        IEnumerator LoadTexture()
        {
            WWW www = new WWW("file://" + filelocationinput); ;

            yield return www;

            Texture2D avatarImage = null;

            if (www.error == null && www.texture != null)
            {
                avatarImage = www.texture;
                //testImage.GetComponent<RawImage>().texture = avatarImage;
                print(avatarImage.GetType());
                tempBlob = avatarImage.EncodeToJPG();
                print(tempBlob);
                Debug.Log("Successfully saved!");
            }
            else
            {
                avatarImage = texNotFound;
                Debug.Log("Texture not found");
                tempBlob = null;
            }
        }
    }

}

