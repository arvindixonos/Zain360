using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zain360
{
    public class TabPanel : MonoBehaviour
    {
        private bool panelSelected = false;
        private int currentSelectedItem = 0;
        public Selectable[] selectables;

        public void PanelSelected()
        {
            panelSelected = true;

            SelectCurrentSelected();
        }

        public void PanelDeselected()
        {
            panelSelected = false;
        }

        public void Tabbed(bool shift = false)
        {
            if (!panelSelected)
                return;

            if (!shift)
            {
                currentSelectedItem += 1;

                if (currentSelectedItem >= selectables.Length)
                {
                    currentSelectedItem = 0;
                }
            }
            else
            {
                currentSelectedItem -= 1;

                if(currentSelectedItem < 0)
                {
                    currentSelectedItem = selectables.Length - 1;
                }
            }

            SelectCurrentSelected();
        }

        void SelectCurrentSelected()
        {
            selectables[currentSelectedItem].Select();
        }
    }
}
