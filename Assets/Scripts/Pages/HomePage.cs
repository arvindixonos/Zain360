using BestHTTP.SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Zain360
{
    public class HomePage : Page
    {
        public GameObject roomsHandle;
        public EditRoom editRoomsHandle;

        public Room[] rooms;

        public Text usernameText;

        private bool editingRoom = false;

        public EditRoom editRoom;
        public TabPanel editRoomTabPanel;

        public override void ShowPage()
        {
            base.ShowPage();

            usernameText.text = GameManager.Instance.currentUserFirstName;

            roomsHandle.SetActive(true);
            editRoomsHandle.gameObject.SetActive(false);

            CancelInvoke("FetchRoomInfos");
            InvokeRepeating("FetchRoomInfos", 0f, 2f);
        }

        public void FetchRoomInfos()
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
                string roomOwner = objects["roomowner"] as string;
                string startTime = objects["starttime"] as string;
                string endTime = objects["endtime"] as string;
                string thumbnail = objects["thumbnail"] as string;
                rooms[i].SetRoom(roomID, roomTitle, roomDescription, roomStatus, roomOwner, startTime, endTime, thumbnail);
                i++;
            }
        }

        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public override void HidePage()
        {
            base.HidePage();

            CancelInvoke("FetchRoomInfos");
        }

        public void JoinRoomClicked(Dictionary<string, object> roomDetails)
        {
            MultiplayerManager.Instance.CallServer("joinroom", null, roomDetails);

            UIManager.Instance.ChangePage(ePages.VIDEO_PAGE);
            UIManager.Instance.SendMessageToCurrentPage("StartStreaming", roomDetails);
        }

        public void StartClassClicked(Dictionary<string, object> roomDetails)
        {
            MultiplayerManager.Instance.CallServer("joinroom", null, roomDetails);

            if (!GameManager.Instance.isStudent)
            {
                MultiplayerManager.Instance.CallServer("setroomstreaming", null, roomDetails);
            }

            UIManager.Instance.ChangePage(ePages.VIDEO_PAGE);
            UIManager.Instance.SendMessageToCurrentPage("StartStreaming", roomDetails);
        }


        public void LogoutClicked()
        {
            GameManager.Instance.Logout();

            UIManager.Instance.ChangePage(ePages.LOGIN_SIGNUP_PAGE);
        }

        public void EditRoomClicked(int roomid)
        {
            editingRoom = true;

            editRoom.ClearAllFields();

            GameManager.Instance.currentSelectedRoomID = roomid;
            roomsHandle.SetActive(false);
            editRoomsHandle.gameObject.SetActive(true);
        }

        public void EditRoomCancelClicked()
        {
            editingRoom = false;

            roomsHandle.SetActive(true);
            editRoomsHandle.gameObject.SetActive(false);
        }

        private Dictionary<string, object> GetCurrentSelectedRoomDetails()
        {
            Dictionary<string, object> roomDetails = new Dictionary<string, object>();
            roomDetails["roomid"] = GameManager.Instance.currentSelectedRoomID;
            roomDetails["title"] = editRoomsHandle.roomTitle.text;
            roomDetails["description"] = editRoomsHandle.roomDescription.text;
            string nowtime = System.DateTime.Now.Date.ToString("MM/dd/yyyy");
            nowtime = nowtime.Replace("-", "/");
            roomDetails["starttime"] = nowtime + " " + editRoomsHandle.starttime.options[editRoomsHandle.starttime.value].text;
            roomDetails["endtime"] = nowtime + " " + editRoomsHandle.endtime.options[editRoomsHandle.endtime.value].text;
            print(roomDetails["starttime"]);
            print(roomDetails["endtime"]);
            if(uFileBrowser.Demo.Instance.tempBlob != null)
            {
                string thumb = Convert.ToBase64String(uFileBrowser.Demo.Instance.tempBlob);
                roomDetails["thumbnail"] = thumb;
            }
            else
            {
                roomDetails["thumbnail"] = "null";
            }
            return roomDetails;
        }

        public void SaveDetailsClicked()
        {
            MultiplayerManager.Instance.CallServer("setroomsinfo", SavedRoomInfoResult, GetCurrentSelectedRoomDetails());
        }

        public void SavedRoomInfoResult(Socket socket, Packet packet, params object[] args)
        {
            editingRoom = false;

            roomsHandle.SetActive(true);
            editRoomsHandle.gameObject.SetActive(false);
            Dictionary<string, object> retObjects = args[0] as Dictionary<string, object>;
            FillRooms(retObjects);
            rooms[GameManager.Instance.currentSelectedRoomID - 1].startClassButton.interactable = true;
        }

        public override void Tabbed(bool shift = false)
        {
            if (editingRoom)
            {
                editRoomTabPanel.Tabbed(shift);
            }
        }
    }
}