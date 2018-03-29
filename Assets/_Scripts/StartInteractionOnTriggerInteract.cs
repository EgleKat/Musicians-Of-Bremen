using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInteractionOnTriggerInteract : MonoBehaviour
{

    bool inTrigger = false;
    bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!inTrigger)
        {
            EventManager.AddListener(EventType.PressedInteractKey, OnPressedInteractKey);
            EventManager.TriggerEvent(EventType.ShowInteractHint, gameObject.name);
            inTrigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (inTrigger)
        {
            EventManager.RemoveListener(EventType.PressedInteractKey, OnPressedInteractKey);
            EventManager.TriggerEvent(EventType.HideInteractHint, gameObject.name);
            inTrigger = false;
        }
    }

    void OnPressedInteractKey(object _)
    {
        if (inTrigger && !triggered)
        {
            triggered = true;
            EventManager.AddListener(EventType.EndInteraction, OnEndInteraction);
            EventManager.RemoveListener(EventType.PressedInteractKey, OnPressedInteractKey);
            EventManager.TriggerEvent(EventType.HideInteractHint, gameObject.name);
            EventManager.TriggerEvent(EventType.StartInteraction, gameObject.name);
        }
    }

    void OnEndInteraction(object triggerName)
    {
        if ((string)triggerName == gameObject.name)
        {
            EventManager.RemoveListener(EventType.EndInteraction, OnEndInteraction);
            triggered = false;

            if (inTrigger)
            {
                EventManager.TriggerEvent(EventType.ShowInteractHint, gameObject.name);
                EventManager.AddListener(EventType.PressedInteractKey, OnPressedInteractKey);
            }
        }
    }

}
