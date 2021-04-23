using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ZainToggle : MonoBehaviour
{
    public string textTitle;
    public Text titleText;
    public Color textSelectedColor = Color.white;
    public Color textUnselectedColor = Color.green;

    private Toggle targetToggle;

    public MonoBehaviour targetMono;
    public string targetFunctionName;

    public void ValueChanged()
    {
        if(targetToggle != null)
        {
            if (targetToggle.isOn)
            {
                ToggleOn();
            }
            else
            {
                ToggleOff();
            }
        }
    }

    public void ToggleOn()
    {
        ChangeToSelectedColor();

        if(targetMono != null)
        {
            targetMono.SendMessage(targetFunctionName);
        }
    }

    public void ChangeToSelectedColor()
    {
        titleText.DOKill();
        titleText.DOColor(textSelectedColor, 0.2f);
    }

    public void ToggleOff()
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
        targetToggle = GetComponent<Toggle>();
        
        if(targetToggle.isOn)
        {
            ChangeToSelectedColor(); 
        }
        else
        {
            ChangeToUnselectedColor();
        }
    }

    void Update()
    {
        
    }
}
