using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace Zain360
{
    public class Page : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        public float bgfadeSpeed = 10f;
        private float endFadeValue = 0.7f;
        public float fadeInDelay = 1f;

        public virtual void Init()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            HidePage();
        }

        public virtual void Awake()
        {
        }

        public virtual void ShowPage()
        {
            canvasGroup.DOFade(1f, 0.5f);
            canvasGroup.blocksRaycasts = true;

        }

        public virtual void HidePage()
        {
            canvasGroup.DOFade(0f, 0.5f);
            canvasGroup.blocksRaycasts = false;

        }
    }
}