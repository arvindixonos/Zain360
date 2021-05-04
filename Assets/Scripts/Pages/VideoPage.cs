using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

        void Awake()
        {
            videoRenderImageAnchorMin = videoRenderImage.rectTransform.anchorMin;
            videoRenderImageAnchorMax = videoRenderImage.rectTransform.anchorMax;
            videoRenderImageSizeDelta = videoRenderImage.rectTransform.sizeDelta;
            videoRenderImageAnchoredPos = videoRenderImage.rectTransform.anchoredPosition;
        }

        public override void ShowPage()
        {
            base.ShowPage();

            StartStreaming();
        }

        public void StartStreaming()
        {
            string roomName = PlayerPrefs.GetString("room_name");

            fullScreenIndicator.gameObject.SetActive(false);
            lookIndicator.SetActive(true);
            videoRenderImage.texture = null;

            UIManager.Instance.ShowLoadingPage();

            GameManager.Instance.StreamRoom(roomName);
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

            UIManager.Instance.ChangePage(ePages.HOME_PAGE);
        }
    }
}