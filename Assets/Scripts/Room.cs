using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Zain360
{
    public class Room : MonoBehaviour
    {
        public int roomID;

        public Text roomTitle;
        public Text roomDescription;
        public Text roomStatus;
        public Text roomOwner;
        public Text classTime;
        public RawImage classthumbnail;
        public Texture2D defaultThumbnail;

        public Button joinClassButton;
        public Button startClassButton;
        public Button editRoomButtom;

        public Color buttonTextSelectedColor;
        public Color buttonTextDisabledColor;

        public Text statusIndicatorText;
        public Image statusIndicator;
        public Sprite availableStatusSprite;
        public Sprite streamingStatusSprite;
        public Sprite offlineStatusSprite;



        private void Awake()
        {
        }

        public void SetRoom(int roomID, string title, string description, string status, string owner, string start, string end, string thumbnail)
        {
            this.roomID = roomID;
            roomTitle.text = title;
            roomDescription.text = description;
            roomStatus.text = status;
            roomOwner.text = owner;
            if(thumbnail != "")
            {
                Texture2D thumb = new Texture2D(128, 128);
                print(thumbnail);
                byte[] blob = Convert.FromBase64String(thumbnail);
                thumb.LoadImage(blob);
                classthumbnail.texture = thumb;
            }
            else
            {
                classthumbnail.texture = defaultThumbnail;
            }
            if (start == "N/A" && end == "N/A")
            {
                classTime.text = "N/A";
            }
            else
            {
                string duration = start.Split(' ')[1] + " - " + end.Split(' ')[1];
                classTime.text = duration;
            }

            if (GameManager.Instance.isStudent)
            {
                startClassButton.gameObject.SetActive(false);
                editRoomButtom.gameObject.SetActive(false);
                joinClassButton.gameObject.SetActive(true);

                if (status == "Streaming")
                {
                    joinClassButton.interactable = true;
                    statusIndicator.sprite = streamingStatusSprite;
                    gameObject.SetActive(true);
                }
                else if (status == "Ready")
                {
                    roomStatus.text = "Ready";
                    statusIndicator.sprite = availableStatusSprite;
                    joinClassButton.interactable = false;
                    startClassButton.interactable = false;
                    editRoomButtom.interactable = true;
                }
                else
                {
                    roomStatus.text = "Offline";
                    statusIndicator.sprite = offlineStatusSprite;
                    joinClassButton.interactable = false;
                    startClassButton.interactable = false;
                    editRoomButtom.interactable = true;
                }
            }
            else
            {
                gameObject.SetActive(true);

                startClassButton.gameObject.SetActive(true);
                editRoomButtom.gameObject.SetActive(true);
                //joinClassButton.gameObject.SetActive(false);

                if (status == "Streaming")
                {
                    statusIndicator.sprite = streamingStatusSprite;

                    startClassButton.interactable = false;
                    editRoomButtom.interactable = false;
                    startClassButton.gameObject.SetActive(false);
                    editRoomButtom.gameObject.SetActive(false);
                    joinClassButton.gameObject.SetActive(true);
                }
                else if(status == "Available")
                {
                    statusIndicator.sprite = availableStatusSprite;

                    startClassButton.interactable = false;
                    editRoomButtom.interactable = true;
                    editRoomButtom.gameObject.SetActive(true);
                }
            }
        }

        public void EditRoomClicked()
        {
            UIManager.Instance.SendMessageToCurrentPage("EditRoomClicked", roomID);
        }

        public void JoinRoomClicked()
        {
            Dictionary<string, object> roomDetails = new Dictionary<string, object>();
            roomDetails["roomid"] = roomID;
            roomDetails["roomtitle"] = roomTitle.text;
           
            UIManager.Instance.SendMessageToCurrentPage("JoinRoomClicked", roomDetails);
        }

        public void StartClassClicked()
        {
            Dictionary<string, object> roomDetails = new Dictionary<string, object>();
            roomDetails["roomid"] = roomID;
            roomDetails["roomtitle"] = roomTitle.text;
            roomDetails["roomowner"] = GameManager.Instance.currentUser;

            UIManager.Instance.SendMessageToCurrentPage("StartClassClicked", roomDetails);
        }
    }
}