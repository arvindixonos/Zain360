using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZainButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string textTitle;
    public Text titleText;
    public Color textSelectedColor = Color.white;
    public Color textUnselectedColor = Color.green;

    public MonoBehaviour targetMono;
    public string targetFunctionName;

    public void OnClicked()
    {
        if (targetMono != null)
        {
            targetMono.SendMessage(targetFunctionName);
        }
    }

    public void ButtonOn()
    {
        ChangeToSelectedColor();
    }

    public void ChangeToSelectedColor()
    {
        titleText.DOKill();
        titleText.DOColor(textSelectedColor, 0.2f);
    }

    public void ButtonOff()
    {
        ChangeToUnselectedColor();
    }

    public void ChangeToUnselectedColor()
    {
        titleText.DOKill();
        titleText.DOColor(textUnselectedColor, 0.2f);
    }

    void Awake()
    {
        titleText.text = textTitle;
        
        ChangeToUnselectedColor();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonOff();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonOn();
    }
}
