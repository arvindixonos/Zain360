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

        public static GameManager Instance;

        public string ipAddress;
        public UniversalMediaPlayer universalMediaPlayer;

        private Quaternion rot = new Quaternion(0, 1, 0, 1);

        public GyroCamera gyroCamera;
        public SimpleRotateSphere rotateCamera;

        private void Start()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            Input.gyro.enabled = true;
            gyroCamera.enabled = true;
            rotateCamera.enabled = false;
#else
            Input.gyro.enabled = false;
            gyroCamera.enabled = false;
            rotateCamera.enabled = true;
#endif

            UIManager.Instance.ChangePage(ePages.LOGIN_SIGNUP_PAGE);

            //StreamClassRoom("class01");
        }

        private void Update()
        {
//#if UNITY_ANDROID 
//            Camera.main.transform.localRotation = Input.gyro.attitude * rot;

//            print(Camera.main.transform.localRotation.eulerAngles + "   " + Input.gyro.attitude.eulerAngles);
//#endif
        }

        public void StopStream()
        {
            universalMediaPlayer.Stop(true);
        }

        public void StreamRoom(string classroomname)
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

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        public void OnDestroy()
        {
            Instance = null;
        }        
    }

}