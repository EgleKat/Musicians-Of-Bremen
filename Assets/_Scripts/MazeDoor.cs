using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoor : MonoBehaviour
{
    public string buttonName;
    public bool startClosed = true;


    private void Awake()
    {
        EventManager.AddListener(EventType.StartInteraction, OnStartInteraction);
        gameObject.SetActive(startClosed);
    }

    private void OnStartInteraction(object name)
    {
        string interactionName = (string)name;

        if (interactionName == buttonName)
        {
            gameObject.SetActive(!startClosed);
            EventManager.TriggerEvent(EventType.PlaySound, "ting");
        }
    }

}
