﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UMP;
using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Events;


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

        private SocketManager socketManager;

        public string address = "http://127.0.0.1:2021/socket.io/";


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

        public void Login(string username, string password)
        {
            Dictionary<string, object> userinfos = new Dictionary<string, object>();
            userinfos["username"] = username;
            userinfos["password"] = password;

            MultiplayerManager.Instance.CallServer("login", LoginResult, userinfos);
        }

        public void LoginResult(Socket socket, Packet packet, params object[] args)
        {
            Dictionary<string, object> retObjects = args[0] as Dictionary<string, object>;

            if (retObjects.Count > 0)
            {
                int retcode = int.Parse(retObjects["retcode"].ToString());
                string retstatus = retObjects["retstatus"].ToString();

                if (retcode == 0)
                {
                    UIManager.Instance.ChangePage(ePages.HOME_PAGE);
                }
                else
                {
                    print(retstatus);
                }
            }
        }

        public void Signup(string firstname, string lastname, string username, string password)
        {
            Dictionary<string, object> userinfos = new Dictionary<string, object>();
            userinfos["firstname"] = firstname;
            userinfos["lastname"] = lastname;
            userinfos["username"] = username;
            userinfos["password"] = password;

            MultiplayerManager.Instance.CallServer("signup", SignupResult, userinfos);
        }


        public void SignupResult(Socket socket, Packet packet, params object[] args)
        {
            print("Signup result received from server");

            Dictionary<string, object> retObjects = args[0] as Dictionary<string, object>;

            if (retObjects.Count > 0)
            {
                int retcode = int.Parse(retObjects["retcode"].ToString());
                string retstatus = retObjects["retstatus"].ToString();

                if(retcode == 0)
                {
                    UIManager.Instance.SendMessageToCurrentPage("SelectLogin");
                }
                else
                {
                    print(retstatus);
                }
            }
        }
    }
}