using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorRegisterEvents : MonoBehaviour {
    void Awake()
    {
        EventManager.AddListener(EventType.GoOutside, OnWentOutside);
        EventManager.AddListener(EventType.GoInside, OnWentInside);
    }

    void OnWentOutside(object _)
    {
        gameObject.SetActive(false);
    }
    void OnWentInside(object _)
    {
        gameObject.SetActive(true);
    }
}
