using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zain360
{
    public class HomePage : Page
    {
        public GameObject   roomsHandle;
        public GameObject   editRoomsHandle;

        public override void ShowPage()
        {
            base.ShowPage();
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