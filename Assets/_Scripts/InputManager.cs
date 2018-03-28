using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public string gameObjectToControl;

    private static readonly Dictionary<KeyCode, Vector3> movementKeys = new Dictionary<KeyCode, Vector3> {
        {KeyCode.W, Vector3.up},
        {KeyCode.S, Vector3.down},
        {KeyCode.D, Vector3.right},
        {KeyCode.A, Vector3.left},
    };

    KeyCode currentMovementKey = KeyCode.None;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventManager.TriggerEvent(EventType.PressedInteractKey, null);
        }

        foreach (KeyValuePair<KeyCode, Vector3> keyVector in movementKeys)
        {
            if (currentMovementKey == KeyCode.None && Input.GetKey(keyVector.Key))
            {
                currentMovementKey = keyVector.Key;
                MoveCommand moveInDirectionCommand = new MoveCommand(gameObjectToControl, movementKeys[currentMovementKey], MoveCommand.MoveType.Direction);
                EventManager.TriggerEvent(EventType.Move, moveInDirectionCommand);
            }
        }
        foreach (KeyValuePair<KeyCode, Vector3> keyVector in movementKeys)
        {
            if (currentMovementKey == keyVector.Key && Input.GetKeyUp(keyVector.Key))
            {
                currentMovementKey = KeyCode.None;
                MoveCommand moveInDirectionCommand = new MoveCommand(gameObjectToControl, null, MoveCommand.MoveType.Direction);
                EventManager.TriggerEvent(EventType.Move, moveInDirectionCommand);
            }
        }
    }
}
