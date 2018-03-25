using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideObject : MonoBehaviour {

   void Awake()
   {

    EventManager.AddListener(EventType.ShowObject, OnShowObject);
    EventManager.AddListener(EventType.HideObject, OnHideObject);

   }

    private void OnHideObject(object arg0)
    {
        gameObject.SetActive(false);
    }

    private void OnShowObject(object arg0)
    {
        gameObject.SetActive(true);
    }

   
}
