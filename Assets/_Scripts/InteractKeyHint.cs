using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractKeyHint : MonoBehaviour {
	void Awake () {
        EventManager.AddListener(EventType.ShowInteractHint, OnTriggerCollide);
        EventManager.AddListener(EventType.HideInteractHint, OnExitTriggerCollide);
        EventManager.AddListener(EventType.PressedInteractKey, OnExitTriggerCollide);
        gameObject.SetActive(false);
    }

    private void OnExitTriggerCollide(object triggerName)
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerCollide(object triggerName)
    {
        gameObject.SetActive(true);
    }
}
