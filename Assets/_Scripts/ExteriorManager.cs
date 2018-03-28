using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorManager : MonoBehaviour
{
    void Awake()
    {
        EventManager.AddListener(EventType.GoOutside, OnWentOutside);
        EventManager.AddListener(EventType.GoInside, OnWentInside);
    }

    void OnWentOutside(object _)
    {
        gameObject.SetActive(true);
    }
    void OnWentInside(object _)
    {
        gameObject.SetActive(false);
    }
}
