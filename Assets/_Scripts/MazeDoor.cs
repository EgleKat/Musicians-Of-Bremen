using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoor : MonoBehaviour
{
    public string buttonName;


    private void Awake()
    {
        EventManager.AddListener(EventType.StartInteraction, OnStartInteraction);
    }

    private void OnStartInteraction(object name)
    {
        string interactionName = (string)name;

        if (interactionName == buttonName)
        {
            gameObject.SetActive(false);
        }
    }

}
