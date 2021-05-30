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

        public Button joinClassButton;
        public Button startClassButton;
        public Button editRoomButtom;

        public Color buttonTextSelectedColor;
        public Color buttonTextDisabledColor;

        public Text statusIndicatorText;
        public Image statusIndicator;
        public Sprite availableStatusSprite;
        public Sprite streamingStatusSprite;



        private void Awake()
        {
        }

        public void SetRoom(int roomID, string title, string description, string status)
        {
            this.roomID = roomID;
            roomTitle.text = title;
            roomDescription.text = description;
            roomStatus.text = status;

            if (GameManager.Instance.isStudent)
            {
                startClassButton.gameObject.SetActive(false);
                editRoomButtom.gameObject.SetActive(false);
                joinClassButton.gameObject.SetActive(true);

                if (status == "Streaming")
                {
                    statusIndicator.sprite = streamingStatusSprite;
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
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

                    startClassButton.interactable = true;
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
    }
}