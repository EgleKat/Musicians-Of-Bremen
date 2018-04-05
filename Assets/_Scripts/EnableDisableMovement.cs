using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridBasedMovement))]
[RequireComponent(typeof(CharacterAnimator))]
public class EnableDisableMovement : MonoBehaviour {

    private GridBasedMovement gridBasedMovement;
    private CharacterAnimator characterAnimator;

    private void Awake()
    {
        EventManager.AddListener(EventType.EnableMovement, OnEnableMovement);
        EventManager.AddListener(EventType.DisableMovement, OnDisableMovement);

        gridBasedMovement = GetComponent<GridBasedMovement>();
        characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void OnDisableMovement(object _)
    {
        gridBasedMovement.enabled = false;
        characterAnimator.enabled = false;
    }

    private void OnEnableMovement(object arg0)
    {
        gridBasedMovement.enabled = true;
        characterAnimator.enabled = true;
    }
}
