using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollower : MonoBehaviour {

    public string gameObjectToFollow;

    private void Awake()
    {
        EventManager.AddListener(EventType.Move, OnMoveCommand);
    }

    private void OnMoveCommand(object moveCommand)
    {
        
    }
}
