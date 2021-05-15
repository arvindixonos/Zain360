using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Zain360
{
    public class Room : MonoBehaviour
    {
        public Text roomTitle;
        public Text roomDescription;
        public Text roomStatus;

        public Button joinRoomButton;
        public Button editRoomButtom;

        public Color buttonTextSelectedColor;
        public Color buttonTextDisabledColor;

        private Text joinRoomButtonText;

        private void Awake()
        {
            joinRoomButtonText = joinRoomButton.GetComponentInChildren<Text>();
        }

        public void TestCall()
        {

        }

        public void SetRoom(string title, string description, string status)
        {
            roomTitle.text = title;
            roomDescription.text = description;
            roomStatus.text = status;

            if(GameManager.Instance.isStudent)
            {
                joinRoomButtonText.text = "Join Class";
                editRoomButtom.gameObject.SetActive(false);

                if (status == "Streaming")
                {
                    joinRoomButton.interactable = true;
                    joinRoomButtonText.color = buttonTextSelectedColor;
                }
                else
                {
                    joinRoomButton.interactable = false;
                    joinRoomButtonText.color = buttonTextDisabledColor;
                }
            }
            else
            {
                joinRoomButtonText.text = "Start Class";
            }
        }
    }
}