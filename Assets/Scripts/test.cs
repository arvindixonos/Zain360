using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    void Start()
    {
    }

    public void OnMouseDown()
    {
        print("Mouse Down");
    }

    public void OnMouseUp()
    {
        print("Mouse Up");
    }

    public void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            //print(OVRInput.Orie(OVRInput.Controller.RHand));
        }
    }
}
