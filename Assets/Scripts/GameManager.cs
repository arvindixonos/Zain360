using System;
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

        public string serverAddress;
        public UniversalMediaPlayer universalMediaPlayer;

        private Quaternion rot = new Quaternion(0, 1, 0, 1);

        public GyroCamera gyroCamera;
        public SimpleRotateSphere rotateCamera;

        private SocketManager socketManager;

        public int currentSelectedRoomID = -1;

        private bool student = true;
        public bool isStudent
        {
            get { return student; }
            set { student = value; }
        }

        public GameObject person1;
        public GameObject person2;


        public string currentUserFirstName;
        public string currentUserLastName;

        public string currentRoomTitle;

        private void Start()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            Input.gyro.enabled = true;
            gyroCamera.enabled = true;
            rotateCamera.enabled = false;
#elif UNITY_IOS && !UNITY_EDITOR
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
            //universalMediaPlayer.Path = "rtmp://" + serverAddress + "/live/" + classroomname;
            //#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            //            //universalMediaPlayer.Path = "http://" + ipAddress + "/dash/" + classroomname + ".mpd";
            //            universalMediaPlayer.Path = "rtmp://" + ipAddress + "/live/" + classroomname;
//#if UNITY_ANDROID
//            universalMediaPlayer.Path = "http://77.232.100.197/hls/class01.m3u8";
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
            if (!student)
            {
                if(currentSelectedRoomID != -1)
                {
                    Dictionary<string, object> roomDetails = new Dictionary<string, object>();
                    roomDetails["roomid"] = currentSelectedRoomID;
                    MultiplayerManager.Instance.CallServer("exitroom", null, roomDetails);
                }
            }

            Instance = null;
        }        

        public void Logout()
        {
            MultiplayerManager.Instance.CallServer("logout", null, null);
        }

        public void Login(string username, string password)
        {
            Dictionary<string, object> userinfos = new Dictionary<string, object>();
            userinfos["username"] = username;
            userinfos["password"] = password;
            userinfos["student"] = student ? 1 : 0;

            MultiplayerManager.Instance.CallServer("login", LoginResult, userinfos);


            //Dictionary<string, object> chatinfos = new Dictionary<string, object>();
            //chatinfos["message"] = "This is my chat message";
            //MultiplayerManager.Instance.CallServer("chatmessage", null, chatinfos);

            //MultiplayerManager.Instance.CallServer("getotherusernames", OtherUsersResult, null);
        }

        public void OtherUsersResult(Socket socket, Packet packet, params object[] args)
        {
            //Dictionary<string, object> retObjects = args[0] as Dictionary<string, object>;

            foreach (List<object> username in args)
            {
                if (username.Count > 0)
                {
                    print(username[0].ToString());
                }
            }

            //if (retObjects.Count > 0)
            //{
                
            //}
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
                    currentUserFirstName = retObjects["firstname"].ToString();
                    currentUserLastName = retObjects["lastname"].ToString();

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
            userinfos["student"] = student ? 1 : 0;

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


        public void StudentSelected()
        {
            student = true;

            person1.SetActive(false);
            person2.SetActive(true);
        }

        public void InstructorSelected()
        {
            student = false;

            person1.SetActive(true);
            person2.SetActive(false);
        }
    }
}