using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
	void Awake () {
        EventManager.AddListener(EventType.Teleport, OnTeleport);
	}

    private void OnTeleport(object moveCommand)
    {
        MoveCommand command = (MoveCommand)moveCommand;

        if (command.gameObjectToMove == gameObject.name)
        {
            transform.position = command.vec.Value;
        }
    }
}
