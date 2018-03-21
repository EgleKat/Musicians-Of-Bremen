using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchInteract : MonoBehaviour {

    public string npcName;
    bool inTrigger = false;

    private void Awake()
    {
        EventManager.AddListener(EventType.PressedInteractKey, OnPressedInteractKey);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        inTrigger = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        inTrigger = false;
    }

    void OnPressedInteractKey(object _)
    {
        if (inTrigger)
        {
            EventManager.AddListener(EventType.EndDialogueWith, OnEndDialogueWith);
            EventManager.RemoveListener(EventType.PressedInteractKey, OnPressedInteractKey);
            EventManager.TriggerEvent(EventType.StartDialogueWith, npcName);
        }
    }

    void OnEndDialogueWith(object npcName)
    {
        if ((string)npcName == this.npcName)
        {
            EventManager.AddListener(EventType.PressedInteractKey, OnPressedInteractKey);
        }
    }

}
