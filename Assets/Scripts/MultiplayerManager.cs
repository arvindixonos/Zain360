using UnityEngine;
using System;
using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Events;

namespace Zain360
{
    public class MultiplayerManager : MonoBehaviour
    {
        public static MultiplayerManager Instance = null;

        private SocketManager socketManager;

        public string address = "http://127.0.0.1:2021/socket.io/";

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
            });

            socketManager.Socket.On(SocketIOEventTypes.Disconnect, (s, p, a) =>
            {
                print("Disconnected from Zain360");
            });

            socketManager.Socket.On("otherchatmessage", (s, p, a) =>
            {

            });
        }

        public void CallServer(string eventName, SocketIOAckCallback callback = null, params object[] args)
        {
            socketManager.Socket.Emit(eventName, callback, args);
        }
    }

}