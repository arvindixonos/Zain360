using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Zain360
{
    public enum ePages
    {
        NONE_PAGE,
        LOGIN_SIGNUP_PAGE,
        FORGOT_RESET_PASSWORD_PAGE,
        HOME_PAGE,
        VIDEO_PAGE
    }


    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance = null;

        public Page[] pages;

        public ePages currentPage = ePages.LOGIN_SIGNUP_PAGE;

        public Page loadingPage;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            InitAllPages();
        }

        public void ShowLoadingPage()
        {
            loadingPage.ShowPage();
        }

        public void HideLoadingPage()
        {
            loadingPage.HidePage();
        }

        public void ChangePage(ePages newPage)
        {
            if (currentPage == newPage)
            {
                print("Same Page Present, not changing page!: " + currentPage);
                return;
            }

            print("Setting new Page: " + newPage);

            if(pages[(int)currentPage] != null)
            {
                pages[(int)currentPage].HidePage();
            }

            currentPage = newPage;

            if (pages[(int)currentPage] != null)
            {
                pages[(int)currentPage].ShowPage();
            }
        }

        private void OnDestroy()
        {
            Instance = null;
        }
       
        public void InitAllPages()
        {
            foreach (Page page in pages)
            {
                if(page != null)
                {
                    page.Init();
                }
            }
            
            if(loadingPage != null)
            {
                loadingPage.Init();
            }
        }

        public void SendMessageToCurrentPage(string functionName, object parameter = null)
        {
            pages[(int)currentPage].SendMessage(functionName, parameter);
        }
    }
}