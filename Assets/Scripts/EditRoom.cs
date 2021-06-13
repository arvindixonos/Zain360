using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zain360
{
    public class EditRoom : MonoBehaviour
    {
        public InputField roomTitle;
        public InputField roomDescription;

        public Dropdown starttime;
        public Dropdown endtime;

        public Text editRoomErrorText;
        public RawImage thumbnail;
        public Texture editclassroom;

        public void ClearAllFields()
        {
            roomTitle.text = "";
            roomDescription.text = "";
            editRoomErrorText.text = "";

            starttime.value = 0;
            endtime.value = 0;
            thumbnail.texture = editclassroom;

        }

        public void SaveDetailsClicked()
        {
            if(roomTitle.text.Trim().Length == 0)
            {
                editRoomErrorText.text = "Room Title cannot be empty";
            }
            else if (roomDescription.text.Trim().Length == 0)
            {
                editRoomErrorText.text = "Room Description cannot be empty";
            }
            else if(starttime.value == 0 || endtime.value == 0)
            {
                editRoomErrorText.text = "Please select start time and end time";                
            }
            else if (endtime.value < starttime.value)
            {
                editRoomErrorText.text = "End time should be greater than start time";
            }
            else
            {
                UIManager.Instance.SendMessageToCurrentPage("SaveDetailsClicked");
            }
        }
    }
}