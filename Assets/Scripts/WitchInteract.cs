using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchInteract : MonoBehaviour {

    bool inTrigger = false;

    private void Awake()
    {
        EventManager.AddListener(EventType.PressedInteractKey, OnPressedInteractKey);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.TriggerEvent(EventType.TriggerCollide, gameObject.name);
        inTrigger = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        EventManager.TriggerEvent(EventType.ExitTriggerCollide, gameObject.name);
        inTrigger = false;
    }

    void OnPressedInteractKey(object _)
    {
        if (inTrigger)
        {
            EventManager.AddListener(EventType.EndDialogueWith, OnEndDialogueWith);
            EventManager.RemoveListener(EventType.PressedInteractKey, OnPressedInteractKey);
            EventManager.TriggerEvent(EventType.StartDialogueWith, gameObject.name);
        }
    }

    void OnEndDialogueWith(object npcName)
    {
        if ((string)npcName == gameObject.name)
        {
            EventManager.AddListener(EventType.PressedInteractKey, OnPressedInteractKey);
        }
    }

}
