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
        public EditRoom editRoomsHandle;

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

            FillRooms(retObjects);
        }

        public void FillRooms(Dictionary<string, object> retObjects)
        {
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

        public void JoinRoomClicked(int roomid)
        {
            Dictionary<string, object> roomDetails = new Dictionary<string, object>();
            roomDetails["roomid"] = roomid;
            MultiplayerManager.Instance.CallServer("joinroom", null, roomDetails);

            if (!GameManager.Instance.isStudent)
            {
                MultiplayerManager.Instance.CallServer("setroomstreaming", null, roomDetails);
            }

            UIManager.Instance.ChangePage(ePages.VIDEO_PAGE);

            UIManager.Instance.SendMessageToCurrentPage("StartStreaming", roomid);
        }

        public void LogoutClicked()
        {
            GameManager.Instance.Logout();

            UIManager.Instance.ChangePage(ePages.LOGIN_SIGNUP_PAGE);
        }

        public void EditRoomClicked(int roomid)
        {
            GameManager.Instance.currentSelectedRoomID = roomid;

            roomsHandle.SetActive(false);
            editRoomsHandle.gameObject.SetActive(true);
        }

        private Dictionary<string, object> GetCurrentSelectedRoomDetails()
        {
            Dictionary<string, object> roomDetails = new Dictionary<string, object>();
            roomDetails["roomid"] = GameManager.Instance.currentSelectedRoomID;
            roomDetails["title"] = editRoomsHandle.roomTitle.text;
            roomDetails["description"] = editRoomsHandle.roomDescription.text;

            return roomDetails;
        }

        public void SaveDetailsClicked()
        {
            MultiplayerManager.Instance.CallServer("setroomsinfo", SavedRoomInfoResult, GetCurrentSelectedRoomDetails());
        }

        public void SavedRoomInfoResult(Socket socket, Packet packet, params object[] args)
        {
            roomsHandle.SetActive(true);
            editRoomsHandle.gameObject.SetActive(false);

            Dictionary<string, object> retObjects = args[0] as Dictionary<string, object>;
            FillRooms(retObjects);
        }
    }
}