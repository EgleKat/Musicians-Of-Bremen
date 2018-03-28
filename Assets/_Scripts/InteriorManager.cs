using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorManager : MonoBehaviour {
    void Awake()
    {
        EventManager.AddListener(EventType.GoOutside, OnWentOutside);
        EventManager.AddListener(EventType.GoInside, OnWentInside);
    }

    void Start()
    {
        gameObject.SetActive(false);
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
