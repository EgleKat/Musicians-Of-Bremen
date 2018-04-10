using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridBasedMovement))]
[RequireComponent(typeof(CharacterAnimator))]
public class EnableDisableMovement : MonoBehaviour
{

    private GridBasedMovement gridBasedMovement;

    private void Awake()
    {
        EventManager.AddListener(EventType.EnableMovement, OnEnableMovement);
        EventManager.AddListener(EventType.DisableMovement, OnDisableMovement);

        gridBasedMovement = GetComponent<GridBasedMovement>();
    }

    private void OnDisableMovement(object _)
    {
        gridBasedMovement.isMovementDisabled = true;
    }

    private void OnEnableMovement(object _)
    {
        gridBasedMovement.isMovementDisabled = false;
    }
}
