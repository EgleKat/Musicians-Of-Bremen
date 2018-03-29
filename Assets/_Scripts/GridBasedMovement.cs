using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBasedMovement : MonoBehaviour
{
    public int speed;
    public string gameObjectToLead;

    private int gridSize = 32;
    private MoveCommand nextCommand = null;

    private Vector3 previousLocation;
    private Vector3? nextLocation = null;

    private Vector3 subpixelDelta = Vector3.zero;

    private bool collidingWith;

    private void Awake()
    {
        EventManager.AddListener(EventType.Move, OnMoveCommand);
        EventManager.AddListener(EventType.AddFollower, OnAddFollower);
        EventManager.AddListener(EventType.RemoveFollower, OnRemoveFollower);
        EventManager.AddListener(EventType.StopMoving, OnStopMoving);

        previousLocation = transform.localPosition;
    }

    private void OnStopMoving(object name)
    {
        string gameObjectName = (string)name;
        if (gameObjectName == gameObject.name)
        {
            nextCommand = null;
            nextLocation = null;
            subpixelDelta = Vector3.zero;
        }
    }

    private void OnRemoveFollower(object arg0)
    {
        gameObjectToLead = "";
    }

    private void OnAddFollower(object arg0)
    {
        String[] followerNames = (String[])arg0;
        if (followerNames[0] == gameObject.name)
        {
            gameObjectToLead = followerNames[1];
        }
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
            if (nextCommand.vec.HasValue)
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
                TriggerFollowerMovement();
                StartAnimation();
            }
            else
            {
                StopAnimation();
            }
        }
        else
        {
            StopAnimation();
        }
    }

    public void StopAnimation()
    {
        EventManager.TriggerEvent(EventType.StopMoveAnimation, new MoveCommand(gameObject.name, null, MoveCommand.MoveType.Direction));
    }

    private void StartAnimation()
    {
        Vector3 directionToMove = SnapTo((nextLocation.Value - transform.localPosition), 90).normalized;
        EventManager.TriggerEvent(EventType.MoveAnimation, new MoveCommand(gameObject.name, directionToMove, MoveCommand.MoveType.Direction));
    }

    private void TriggerFollowerMovement()
    {
        MoveCommand followerMoveCommand = new MoveCommand(gameObjectToLead, previousLocation, MoveCommand.MoveType.Location);
        EventManager.TriggerEvent(EventType.Move, followerMoveCommand);
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
        if (!collidingWith && !collider.isTrigger)
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

    private static Vector3 SnapTo(Vector3 v3, float snapAngle)
    {
        float angle = Vector3.Angle(v3, Vector3.up);
        if (angle < snapAngle / 2.0f)          // Cannot do cross product 
            return Vector3.up * v3.magnitude;  //   with angles 0 & 180
        if (angle > 180.0f - snapAngle / 2.0f)
            return Vector3.down * v3.magnitude;

        float t = Mathf.Round(angle / snapAngle);
        float deltaAngle = (t * snapAngle) - angle;

        Vector3 axis = Vector3.Cross(Vector3.up, v3);
        Quaternion q = Quaternion.AngleAxis(deltaAngle, axis);
        return Vector3Int.RoundToInt((q * v3).normalized);
    }
}
