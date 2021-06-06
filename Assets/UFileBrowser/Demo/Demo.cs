using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace uFileBrowser
{

    public class Demo : UnityEngine.MonoBehaviour
    {

        public UnityEngine.UI.InputField IF;
        Texture2D texNotFound, resized;
        private string filelocationinput = "";
        public RawImage testImage;
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

        Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
        {
            RenderTexture rt = new RenderTexture(targetX, targetY, 24);
            RenderTexture.active = rt;
            Graphics.Blit(texture2D, rt);
            Texture2D result = new Texture2D(targetX, targetY);
            result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
            result.Apply();
            return result;
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
                resized = Resize(avatarImage, 128, 128);
                print(avatarImage.GetType());
                tempBlob = resized.EncodeToJPG();
                print(tempBlob);
                Texture2D simple = new Texture2D(128, 128);
                simple.LoadImage(tempBlob);
                testImage.GetComponent<RawImage>().texture = resized;
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

