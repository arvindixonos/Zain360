using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    void Start()
    {
        string currentTime = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm");
        string todayTime = System.DateTime.Today.ToString("MM/dd/yyyy HH:mm");

        System.DateTime dt = System.DateTime.Parse(currentTime);
    }

    void Update()
    {
        
    }
}
