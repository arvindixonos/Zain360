using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UMP;


namespace Zain360
{
    public class GameManager : MonoBehaviour
    {
        public Material domeMaterial;

        public Canvas UICanvas;
        public Text updateText;


        public string ipAddress;
        public UniversalMediaPlayer universalMediaPlayer;

        private Quaternion rot = new Quaternion(0, 1, 0, 1);

        public void ShowCanvas()
        {
            UICanvas.gameObject.SetActive(true);
        }

        public void HideCanvas()
        {
            UICanvas.gameObject.SetActive(false);
        }

        public void PathPrepared(String path)
        {
            print("Path Prepared: " + path);
            //updateText.text = "Path Prepared";
        }

        public void Opening()
        {
            print("Opening");
        }

        public void Buffering(float percentage)
        {
            print("Buffering: " + percentage);
            //updateText.text = "Buffering";
        }

        public void ImageReady(UnityEngine.Object texture)
        {
            print("Image Ready");
        }

        public void Prepared(int width, int height)
        {
            print("Prepared: " + width + "x" + height);
        }

        public void Playing()
        {
            print("Playing");

            //HideCanvas();
        }

        public void OnError()
        {
            print("SOME ERROR!");
        }

        private void Start()
        {
#if UNITY_ANDROID
            Input.gyro.enabled = true;
#endif

            UIManager.Instance.ChangePage(ePages.HOME_PAGE);

            //StreamClassRoom("class01");
        }

        private void Update()
        {
//#if UNITY_ANDROID 
//            Camera.main.transform.localRotation = Input.gyro.attitude * rot;

//            print(Camera.main.transform.localRotation.eulerAngles + "   " + Input.gyro.attitude.eulerAngles);
//#endif
        }

        public void StreamClassRoom(string classroomname)
        {
            universalMediaPlayer.Path = "rtmp://" + ipAddress + "/live/" + classroomname;
//#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
//            //universalMediaPlayer.Path = "http://" + ipAddress + "/dash/" + classroomname + ".mpd";
//            universalMediaPlayer.Path = "rtmp://" + ipAddress + "/live/" + classroomname;
//#elif UNITY_ANDROID
//            universalMediaPlayer.Path = "http://" + ipAddress + "/hls/" + classroomname + ".m3u8";
//#endif
            universalMediaPlayer.Play();
        }
    }

}