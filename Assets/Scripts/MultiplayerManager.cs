using UnityEngine;
using System;
using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Events;
using System.Collections.Generic;

namespace Zain360
{
    public class MultiplayerManager : MonoBehaviour
    {
        public static MultiplayerManager Instance = null;

        private SocketManager socketManager;

        public string address = "http://127.0.0.1:2021/socket.io/";

        public GameObject reconnectingIndicator;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Init();
        }

        private void Start()
        {
            
        }

        public void OnDestroy()
        {
            Instance = null;

            if (this.socketManager != null)
            {
                // Leaving this sample, close the socket
                this.socketManager.Close();
                this.socketManager = null;
            }
        }

        protected void Init()
        {
            socketManager = new SocketManager(new Uri(address));

            socketManager.Socket.On(SocketIOEventTypes.Connect, (s, p, a) =>
            {
                print("Connected to Zain360");

                CancelInvoke("CheckSocketConnection");
                InvokeRepeating("CheckSocketConnection", 2f, 2f);
            });

            socketManager.Socket.On(SocketIOEventTypes.Disconnect, (s, p, a) =>
            {
                print("Disconnected from Zain360");
            });

            socketManager.Socket.On("otherchatmessage", (s, p, a) =>
            {
                string message = a[0] as string;

                UIManager.Instance.SendMessageToCurrentPage("OpponentMessageReceived", message);
            });

            socketManager.Socket.On("mutevideo", (s, p, a) =>
            {
                string message = a[0] as string;

                GameManager.Instance.MuteVideo();
            });

            socketManager.Socket.On("unmutevideo", (s, p, a) =>
            {
                string message = a[0] as string;

                GameManager.Instance.UnmuteVideo();
            });

            socketManager.Socket.On("muteaudio", (s, p, a) =>
            {
                string message = a[0] as string;

                GameManager.Instance.MuteAudio();
            });

            socketManager.Socket.On("unmuteaudio", (s, p, a) =>
            {
                string message = a[0] as string;

                GameManager.Instance.UnmuteAudio();
            });

            socketManager.Socket.On("exitroom", (s, p, a) =>
            {
                string message = a[0] as string;

                UIManager.Instance.SendMessageToCurrentPage("ExitClass", message);
            });

            socketManager.Socket.On("allroomusers", (s, p, a) =>
            {
                Dictionary<string, object> roomusers = a[0] as Dictionary<string, object>;

                List<string> usernames = new List<string>();

                foreach(KeyValuePair<string, object> roomuser in roomusers)
                {
                    Dictionary<string, object> userinfo = roomuser.Value as Dictionary<string, object>;

                    string combinedName = userinfo["firstname"] as string + " " + userinfo["lastname"] as string;

                    usernames.Add(combinedName);
                }

                UIManager.Instance.SendMessageToCurrentPage("ParticipantsListReceived", usernames);
            });

        }

        public void CheckSocketConnection()
        {
            if (socketManager != null && reconnectingIndicator != null)
            {
                reconnectingIndicator.SetActive(socketManager.State == SocketManager.States.Reconnecting ||
                                                socketManager.State == SocketManager.States.Opening);
            }
        }

        public void CallServer(string eventName, SocketIOAckCallback callback = null, params object[] args)
        {
            if(socketManager.State == SocketManager.States.Open)
            {
                socketManager.Socket.Emit(eventName, callback, args);
            }
        }
    }

}