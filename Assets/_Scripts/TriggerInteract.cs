using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteract : MonoBehaviour {

    bool inTrigger = false;

    private void Awake()
    {
        EventManager.AddListener(EventType.PressedInteractKey, OnPressedInteractKey);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.TriggerEvent(EventType.ShowInteractHint, gameObject.name);
        inTrigger = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        EventManager.TriggerEvent(EventType.HideInteractHint, gameObject.name);
        inTrigger = false;
    }

    void OnPressedInteractKey(object _)
    {
        if (inTrigger)
        {
            EventManager.AddListener(EventType.EndDialogue, OnEndDialogueWith);
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
