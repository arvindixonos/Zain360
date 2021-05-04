using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public RectTransform fullscreentransform;

    void Start()
    {
        fullscreentransform.anchorMin = Vector2.zero;
        fullscreentransform.anchorMax = Vector2.one;
        fullscreentransform.sizeDelta = Vector2.zero;
        fullscreentransform.anchoredPosition = Vector2.zero;
    }

    void Update()
    {
        
    }
}
