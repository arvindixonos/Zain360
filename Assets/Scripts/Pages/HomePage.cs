using BestHTTP.SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zain360
{
    public class HomePage : Page
    {
        public GameObject roomsHandle;
        public GameObject editRoomsHandle;

        public Room[] rooms;

        public override void ShowPage()
        {
            base.ShowPage();

            FetchRoomInfos();
        }

        private void FetchRoomInfos()
        {
            MultiplayerManager.Instance.CallServer("getallroomsinfo", RoomInfosResult, null);
        }

        public void RoomInfosResult(Socket socket, Packet packet, params object[] args)
        {
            Dictionary<string, object> retObjects = args[0] as Dictionary<string, object>;

            int i = 0;
            foreach (KeyValuePair<string, object> entry in retObjects)
            {
                Dictionary<string, object> objects = entry.Value as Dictionary<string, object>;
                int roomID = Convert.ToInt32(objects["roomid"]);
                string roomTitle = objects["title"] as string;
                string roomDescription = objects["description"] as string;
                string roomStatus = objects["status"] as string;

                rooms[i].SetRoom(roomID, roomTitle, roomDescription, roomStatus);

                i++;
            }
        }

        public override void HidePage()
        {
            base.HidePage();
        }

        public void JoinRoomClicked()
        {
            PlayerPrefs.SetString("room_name", "class01");
            PlayerPrefs.Save();

            UIManager.Instance.ChangePage(ePages.VIDEO_PAGE);
        }

        public void LogoutClicked()
        {
            GameManager.Instance.Logout();

            UIManager.Instance.ChangePage(ePages.LOGIN_SIGNUP_PAGE);
        }

        public void EditRoomClicked()
        {
            roomsHandle.SetActive(false);
            editRoomsHandle.SetActive(true);
        }

        public void SaveDetailsClicked()
        {
            roomsHandle.SetActive(true);
            editRoomsHandle.SetActive(false);
        }
    }
}