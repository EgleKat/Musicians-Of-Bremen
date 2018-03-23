using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBasedMovement : MonoBehaviour
{
    public int speed;

    private int gridSize = 32;
    private MoveCommand nextCommand = null;

    private Vector3 previousLocation;
    private Vector3? nextLocation = null;

    private Vector3 subpixelDelta = Vector3.zero;

    private bool collidingWith;

    private void Awake()
    {
        EventManager.AddListener(EventType.Move, OnMoveCommand);
        previousLocation = transform.localPosition;
    }

    private void Update()
    {
        if (transform.localPosition == nextLocation)
        {
            UpdateNextLocation();
        }
        if (!nextLocation.HasValue)
        {
            return;
        }

        MoveTowardsNextLocation();
    }

    private void MoveTowardsNextLocation()
    {
        Vector3 directionToMove = (nextLocation.Value - transform.localPosition).normalized;
        Vector3 targetPosition = transform.localPosition + subpixelDelta + Time.deltaTime * speed * directionToMove;

        if (Vector3.Magnitude(targetPosition - transform.localPosition) > Vector3.Magnitude(nextLocation.Value - transform.localPosition))
        {
            // if we've overshot where we're going towards
            targetPosition = nextLocation.Value;
        }

        Vector3 pixelRoundedNewPosition = Vector3Int.RoundToInt(targetPosition);

        subpixelDelta = targetPosition - pixelRoundedNewPosition;
        transform.localPosition = pixelRoundedNewPosition;
    }

    private void UpdateNextLocation()
    {
        previousLocation = transform.localPosition;
        subpixelDelta = Vector3.zero;
        nextLocation = null;
        if (nextCommand != null)
        {
            switch (nextCommand.moveType)
            {
                case MoveCommand.MoveType.Direction:
                    nextLocation = previousLocation + gridSize * nextCommand.vec;
                    break;
                case MoveCommand.MoveType.Location:
                    nextLocation = nextCommand.vec;
                    nextCommand = null;
                    break;
            }
        }
    }

    private void OnMoveCommand(object moveCommand)
    {
        MoveCommand command = (MoveCommand)moveCommand;

        if (command.gameObjectToMove == gameObject.name)
        {
            nextCommand = command;
            if (!nextLocation.HasValue || nextCommand.moveType == MoveCommand.MoveType.Location)
            {
                UpdateNextLocation();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collidingWith)
        {
            collidingWith = true;
            MoveCommand command = new MoveCommand(gameObject.name, previousLocation, MoveCommand.MoveType.Location);
            EventManager.TriggerEvent(EventType.Move, command);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        collidingWith = false;
    }
}
