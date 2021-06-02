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
    
        public void ClearAllFields()
        {
            roomTitle.text = "";
            roomDescription.text = "";

            starttime.value = 0;
            endtime.value = 0;
        }
    }
}