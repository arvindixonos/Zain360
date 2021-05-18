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

        public Button joinRoomButton;
        public Button editRoomButtom;

        public Color buttonTextSelectedColor;
        public Color buttonTextDisabledColor;

        private Text joinRoomButtonText;

        public Text statusIndicatorText;
        public Image statusIndicator;
        public Sprite availableStatusSprite;
        public Sprite streamingStatusSprite;



        private void Awake()
        {
            joinRoomButtonText = joinRoomButton.GetComponentInChildren<Text>();
        }

        public void SetRoom(int roomID, string title, string description, string status)
        {
            this.roomID = roomID;
            roomTitle.text = title;
            roomDescription.text = description;
            roomStatus.text = status;

            if (GameManager.Instance.isStudent)
            {
                joinRoomButtonText.text = "Join Class";
                editRoomButtom.gameObject.SetActive(false);

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

                if (status == "Streaming")
                {
                    statusIndicator.sprite = streamingStatusSprite;

                    joinRoomButtonText.text = "Start Class";
                    joinRoomButton.interactable = false;
                    editRoomButtom.interactable = false;
                    editRoomButtom.gameObject.SetActive(true);
                }
                else if(status == "Available")
                {
                    statusIndicator.sprite = availableStatusSprite;

                    joinRoomButtonText.text = "Start Class";
                    joinRoomButton.interactable = true;
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
            UIManager.Instance.SendMessageToCurrentPage("JoinRoomClicked", roomID);
        }
    }
}