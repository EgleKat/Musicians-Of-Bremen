using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridBasedMovement))]
public class EnableDisableMovement : MonoBehaviour {

    private GridBasedMovement gridBasedMovement;

    private void Awake()
    {
        EventManager.AddListener(EventType.EnableMovement, OnEnableMovement);
        EventManager.AddListener(EventType.DisableMovement, OnDisableMovement);

        gridBasedMovement = GetComponent<GridBasedMovement>();
    }

    private void OnDisableMovement(object _)
    {
        gridBasedMovement.StopAnimation();
        gridBasedMovement.enabled = false;
    }

    private void OnEnableMovement(object arg0)
    {
        gridBasedMovement.enabled = true;
    }
}
