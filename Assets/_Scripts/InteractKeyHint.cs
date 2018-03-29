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
        gameObject.SetActive(false);
    }

    private void OnHideInteractHint(object triggerName)
    {
        gameObject.SetActive(false);
    }

    private void OnShowInteractHint(object triggerName)
    {
        gameObject.SetActive(true);
    }
}
