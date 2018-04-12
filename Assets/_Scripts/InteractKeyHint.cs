using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractKeyHint : MonoBehaviour
{
    void Awake()
    {
        EventManager.AddListener(EventType.ShowInteractHint, OnShowInteractHint);
        EventManager.AddListener(EventType.HideInteractHint, OnHideInteractHint);
    }

    private void OnHideInteractHint(object triggerName)
    {
        EventManager.TriggerEvent(EventType.FadeOut, new FadeCommand("E Hint", 0.2f));
    }

    private void OnShowInteractHint(object triggerName)
    {
        EventManager.TriggerEvent(EventType.FadeIn, new FadeCommand("E Hint", 0.2f));
    }
}
