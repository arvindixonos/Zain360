using EnhancedUI.EnhancedScroller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace Zain360
{
    public class VideoPage : Page
    {
        public RenderTexture videoRenderTexture;
        public RawImage videoRenderImage;

        private Vector2 videoRenderImageAnchorMin = Vector2.zero;
        private Vector2 videoRenderImageAnchorMax = Vector2.zero;
        private Vector2 videoRenderImageSizeDelta = Vector2.zero;
        private Vector2 videoRenderImageAnchoredPos = Vector2.zero;

        public Image fullScreenIndicator;
        public bool fullscreen = false;

        public GameObject lookIndicator;

        private int currentRoomID = -1;

        public ChatDelegate chatDelegate;
        public ParticipantDelegate participantDelegate;

        public GameObject participantScrollerWindow;

        public GameObject chatScrollerWindow;

        public Text usernameText;
        public Text classTitleText;

        public RectTransform chatParticipantWindow;
        private bool chatParticipantWindowShown = false;

        public Toggle chatToggle;
        public Toggle participantToggle;

        void Awake()
        {
            videoRenderImageAnchorMin = videoRenderImage.rectTransform.anchorMin;
            videoRenderImageAnchorMax = videoRenderImage.rectTransform.anchorMax;
            videoRenderImageSizeDelta = videoRenderImage.rectTransform.sizeDelta;
            videoRenderImageAnchoredPos = videoRenderImage.rectTransform.anchoredPosition;
        }

        public override void Start()
        {
            base.Start();

            chatScrollerWindow.gameObject.SetActive(false);
            participantScrollerWindow.gameObject.SetActive(false);
        }

        
        public override void ShowPage()
        {
            base.ShowPage();
        }

        public void StartStreaming(Dictionary<string, object> roomDetails)
        {
            currentRoomID = (int)(roomDetails["roomid"]);

            usernameText.text = GameManager.Instance.currentUserFirstName + " " + GameManager.Instance.currentUserLastName;
            classTitleText.text = roomDetails["roomtitle"].ToString();

            fullScreenIndicator.gameObject.SetActive(false);
            lookIndicator.SetActive(true);
            videoRenderImage.texture = null;

            UIManager.Instance.ShowLoadingPage();

            GameManager.Instance.StreamRoom("Test", currentRoomID);
        }

        public override void HidePage()
        {
            base.HidePage();
        }

        public void ShowCanvas()
        {
        }

        public void HideCanvas()
        {
        }


        /// <summary>
        /// Button handler sending message
        /// </summary>
        public void SendButtonClicked()
        {
            chatDelegate.SendClicked(currentRoomID);
        }

        public void FullScreenClicked()
        {
            fullscreen = !fullscreen;

            if(fullscreen)
            {
                SetFullScreen();
            }
            else
            {
                SetHalfScreen();
            }
        }

        void SetFullScreen()
        {
            videoRenderImage.rectTransform.anchorMin = Vector2.zero;
            videoRenderImage.rectTransform.anchorMax = Vector2.one;
            videoRenderImage.rectTransform.sizeDelta = Vector2.zero;
            videoRenderImage.rectTransform.anchoredPosition = Vector2.zero;
        }

        void SetHalfScreen()
        {
            videoRenderImage.rectTransform.anchorMin = videoRenderImageAnchorMin;
            videoRenderImage.rectTransform.anchorMax = videoRenderImageAnchorMax;
            videoRenderImage.rectTransform.sizeDelta = videoRenderImageSizeDelta;
            videoRenderImage.rectTransform.anchoredPosition = videoRenderImageAnchoredPos;
        }

        public void PathPrepared(String path)
        {
            print("Path Prepared: " + path);
            //updateText.text = "Path Prepared";
        }

        public void Opening()
        {
            print("Opening");
        }

        public void Buffering(float percentage)
        {
            print("Buffering: " + percentage);
            //updateText.text = "Buffering";
        }

        public void ImageReady(UnityEngine.Object texture)
        {
            print("Image Ready");
        }

        public void Prepared(int width, int height)
        {
            print("Prepared: " + width + "x" + height);
        }

        public void Playing()
        {
            print("Playing");

            UIManager.Instance.HideLoadingPage();

            videoRenderImage.texture = videoRenderTexture;
            lookIndicator.SetActive(false);

            fullScreenIndicator.gameObject.SetActive(true);

            //HideCanvas();
        }

        public void OnError()
        {
            print("SOME ERROR!");
        }

        public void EndClassClicked()
        {
            GameManager.Instance.StopStream();

            videoRenderImage.texture = null;
            lookIndicator.SetActive(true);

            Dictionary<string, object> roomDetails = new Dictionary<string, object>();
            roomDetails["roomid"] = currentRoomID;
            MultiplayerManager.Instance.CallServer("exitroom", null, roomDetails);

            if (!GameManager.Instance.isStudent)
            {
                MultiplayerManager.Instance.CallServer("setroomavailable", null, roomDetails);
            }

            UIManager.Instance.ChangePage(ePages.HOME_PAGE);
        }

        void OpponentMessageReceived(string message)
        {
            chatDelegate.OpponentMessageReceived(message);
        }

        void ParticipantsListReceived(List<string> participantsListReceived)
        {
            participantDelegate.ParticipantsListReceived(participantsListReceived);
        }

        public void ChatClicked()
        {
            chatParticipantWindowShown = !chatParticipantWindowShown;

            if(chatParticipantWindowShown)
            {
                ShowChatParticipantWindow();
            }
            else
            {
                HideChatParticipantWindow();
            }

            chatToggle.isOn = true;
            participantToggle.isOn = false;
        }

        public void ParticipantClicked()
        {
            chatParticipantWindowShown = !chatParticipantWindowShown;

            if (chatParticipantWindowShown)
            {
                ShowChatParticipantWindow();
            }
            else
            {
                HideChatParticipantWindow();
            }

            chatToggle.isOn = false;
            participantToggle.isOn = true;
        }

        public void ShowChatParticipantWindow()
        {
            chatParticipantWindow.DOKill();
            chatParticipantWindow.DOAnchorPosX(0f, 1f);

            chatParticipantWindowShown = true;
        }

        public void HideChatParticipantWindow()
        {
            chatParticipantWindow.DOKill();
            chatParticipantWindow.DOAnchorPosX(500f, 1f);

            chatParticipantWindowShown = false;
        }

        public void ChatToggled()
        {
            chatScrollerWindow.gameObject.SetActive(true);
            participantScrollerWindow.gameObject.SetActive(false);
        }

        public void ParticipantToggled()
        {
            chatScrollerWindow.gameObject.SetActive(false);
            participantScrollerWindow.gameObject.SetActive(true);
        }
    }
}