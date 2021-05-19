using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zain360
{
    public class ChatDelegate : MonoBehaviour, IEnhancedScrollerDelegate
    {

        /// <summary>
        /// Internal representation of our data. Note that the scroller will never see
        /// this, so it separates the data from the layout using MVC principles.
        /// </summary>
        private List<DataChat> _data;

        /// <summary>
        /// This member tells the scroller that we need
        /// the cell views to figure out how much space to use.
        /// This is only set to true on the first pass to reduce
        /// processing required.
        /// </summary>
        private bool _calculateLayout;

        /// <summary>
        /// This stores the total size of all the cells,
        /// plus the scroller's top and bottom padding.
        /// This will be used to calculate the spacer required
        /// </summary>
        private float _totalCellSize = 0;

        /// <summary>
        /// Stores the scroller's position before jumping to the new chat cell
        /// </summary>
        private float _oldScrollPosition = 0;

        /// <summary>
        /// This is our scroller we will be a delegate for
        /// </summary>
        public EnhancedScroller scroller;

        /// <summary>
        /// The input field for texts from us
        /// </summary>
        public UnityEngine.UI.InputField myInputField;

        /// <summary>
        /// This will be the prefab of our chat cell
        /// </summary>
        public EnhancedScrollerCellView myTextCellViewPrefab;

        /// <summary>
        /// This will be the prefab of another person's chat cell
        /// </summary>
        public EnhancedScrollerCellView otherTextCellViewPrefab;

        /// <summary>
        /// This will be the prefab of our first cell to push the other cells to the bottom
        /// </summary>
        public EnhancedScrollerCellView spacerCellViewPrefab;


        void Start()
        {
            // tell the scroller that this script will be its delegate
            scroller.Delegate = this;

            // set up a single data item containing the spacer
            // this pushes the cells down to the bottom
            _data = new List<DataChat>();
            _data.Add(new DataChat() { cellType = DataChat.CellType.Spacer });

            // call resize scroller to calculate and set up the scroll
            ResizeScroller();

            // focus on the chat input field
            myInputField.ActivateInputField();
        }

        public void AddNewRow(DataChat.CellType cellType, string text)
        {
            // first, clear out the cells in the scroller so the new text transforms will be reset
            scroller.ClearAll();

            _oldScrollPosition = scroller.ScrollPosition;

            // reset the scroller's position so that it is not outside of the new bounds
            scroller.ScrollPosition = 0;

            // second, reset the data's cell view sizes
            for (var i = 1; i < _data.Count; i++)
            {
                _data[i].cellSize = 0;
            }

            // now we can add the data row
            _data.Add(new DataChat()
            {
                cellType = cellType,
                cellSize = 0,
                someText = text
            });

            ResizeScroller();

            // jump to the end of the scroller to see the new content.
            // once the jump is completed, reset the spacer's size
            scroller.JumpToDataIndex(_data.Count - 1, 1f, 1f, tweenType: EnhancedScroller.TweenType.easeInOutSine, tweenTime: 0.5f, jumpComplete: ResetSpacer);
        }

        /// <summary>
        /// This method is called when the new cell has been jumpped to.
        /// It will reset the spacer's cell size to the remainder of the scroller's size minus the
        /// total cell size calculated in ResizeScroller. Finally, it will reload the
        /// scroller to set the new cell sizes.
        /// </summary>
        private void ResetSpacer()
        {
            // reset the spacer's cell size to the scroller's size minus the rest of the cell sizes
            // (or zero if the spacer is no longer needed)
            _data[0].cellSize = Mathf.Max(scroller.ScrollRectSize - _totalCellSize, 0);

            // reload the data to set the new cell size
            scroller.ReloadData(1.0f);
        }

        /// <summary>
        /// This function will expand the scroller to accommodate the cells, reload the data to calculate the cell sizes,
        /// reset the scroller's size back, then reload the data once more to display the cells.
        /// </summary>
        private void ResizeScroller()
        {
            // capture the scroll rect size.
            // this will be used at the end of this method to determine the final scroll position
            var scrollRectSize = scroller.ScrollRectSize;

            // capture the scroller's position so we can smoothly scroll from it to the new cell
            var offset = _oldScrollPosition - scroller.ScrollSize;

            // capture the scroller dimensions so that we can reset them when we are done
            var rectTransform = scroller.GetComponent<RectTransform>();
            var size = rectTransform.sizeDelta;

            // set the dimensions to the largest size possible to accommodate all the cells
            rectTransform.sizeDelta = new Vector2(size.x, float.MaxValue);

            // First Pass: reload the scroller so that it can populate the text UI elements in the cell view.
            // The content size fitter will determine how big the cells need to be on subsequent passes.
            _calculateLayout = true;
            scroller.ReloadData();

            // calculate the total size required by all cells. This will be used when we determine
            // where to end up at after we reload the data on the second pass.
            _totalCellSize = scroller.padding.top + scroller.padding.bottom;
            for (var i = 1; i < _data.Count; i++)
            {
                _totalCellSize += _data[i].cellSize + (i < _data.Count - 1 ? scroller.spacing : 0);
            }

            // set the spacer to the entire scroller size.
            // this is necessary because we need some space to actually do a jump
            _data[0].cellSize = scrollRectSize;

            // reset the scroller size back to what it was originally
            rectTransform.sizeDelta = size;

            // Second Pass: reload the data once more with the newly set cell view sizes and scroller content size.
            _calculateLayout = false;
            scroller.ReloadData();

            // set the scroll position to the previous cell (plus the offset of where the scroller currently is) so that we can jump to the new cell.
            scroller.ScrollPosition = (_totalCellSize - _data[_data.Count - 1].cellSize) + offset;
        }

        public void SendClicked(int currentRoomID)
        {
            // add a chat row from us
            AddNewRow(DataChat.CellType.MyText, myInputField.text);

            Dictionary<string, object> messageInfos = new Dictionary<string, object>();
            messageInfos["roomid"] = currentRoomID;
            messageInfos["message"] = myInputField.text;
            MultiplayerManager.Instance.CallServer("chatmessage", null, messageInfos);

            // clear the input field and focus on it
            myInputField.text = "";
            myInputField.ActivateInputField();
        }

        public void OpponentMessageReceived(string message)
        {
            AddNewRow(DataChat.CellType.OtherText, message);
        }

        #region EnhancedScroller Handlers

        /// <summary>
        /// This tells the scroller the number of cells that should have room allocated. This should be the length of your data array.
        /// </summary>
        /// <param name="scroller">The scroller that is requesting the data size</param>
        /// <returns>The number of cells</returns>
        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            // in this example, we just pass the number of our data elements
            return _data.Count;
        }

        /// <summary>
        /// Gets the cell view size for each cell
        /// </summary>
        /// <param name="scroller"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            // return the cell size for each cell
            return _data[dataIndex].cellSize;
        }

        /// <summary>
        /// Reuse the appropriate cell
        /// </summary>
        /// <param name="scroller"></param>
        /// <param name="dataIndex"></param>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CellViewChat cellView;

            if (dataIndex == 0)
            {
                // this is the first spacer cell
                cellView = scroller.GetCellView(spacerCellViewPrefab) as CellViewChat;
                cellView.name = "Spacer";
            }
            else
            {
                // this is a chat cell

                if (_data[dataIndex].cellType == DataChat.CellType.MyText)
                {
                    // this is one of our chat cells
                    cellView = scroller.GetCellView(myTextCellViewPrefab) as CellViewChat;
                }
                else
                {
                    // this is a chat cell from another person
                    cellView = scroller.GetCellView(otherTextCellViewPrefab) as CellViewChat;
                }

                // set the cell's game object name. Not necessary, but nice for debugging.
                cellView.name = "Cell " + dataIndex.ToString();

                // initialize the cell's data so that it can configure its view.
                cellView.SetData(_data[dataIndex], _calculateLayout);
            }

            return cellView;
        }

        #endregion
    }
}