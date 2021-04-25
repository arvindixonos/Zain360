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

        public GameObject lookIndicator;

        public override void ShowPage()
        {
            base.ShowPage();

            StartStreaming();
        }

        public void StartStreaming()
        {
            string roomName = PlayerPrefs.GetString("room_name");

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