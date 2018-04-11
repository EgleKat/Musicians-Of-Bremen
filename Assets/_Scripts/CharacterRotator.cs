using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour {

    private bool isRotating = false;

    private void Awake()
    {
        EventManager.AddListener(EventType.StartRotating, OnStartRotating);
        EventManager.AddListener(EventType.EndRotating, OnEndRotating);
    }

    private async void OnEndRotating(object name)
    {
        if((string)name == gameObject.name)
        {
            isRotating = false;
            await Wait.ForSeconds(0.01f);
            transform.localRotation = Quaternion.identity;
        }
    }

    private async void OnStartRotating(object name)
    {
        if ((string)name == gameObject.name)
        {
            isRotating = true;
            while (isRotating)
            {
                transform.localRotation *= Quaternion.Euler(0, 0, 10);
                await Wait.ForSeconds(0.05f);
            }
        }
    }
}
