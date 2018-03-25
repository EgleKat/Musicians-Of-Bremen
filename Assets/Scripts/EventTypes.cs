using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    GoOutside,
    GoInside,
    PressedInteractKey,
    StartDialogueWith,
    EndDialogue,
    Move,
    MoveAnimation,
    StopMoveAnimation,
    TriggerCollide,
    ExitTriggerCollide,
    ShowInteractHint,
    HideInteractHint,
    ShowObject,
    HideObject,
    DisplayDialogue,
    AddFollower,
    RemoveFollower,
    Teleport,
    DisableMovement,
    EnableMovement,
    StopMoving,
}

[System.Serializable]
public class UnityEventWithObject : UnityEvent<object> { }

public class MoveCommand
{
    public enum MoveType {Direction, Location}

    public string gameObjectToMove;
    public Vector3? vec;
    public MoveType moveType;


    public MoveCommand(string gameObjectToMove, Vector3? vec, MoveType moveType)
    {
        this.gameObjectToMove = gameObjectToMove;
        this.vec = vec;
        this.moveType = moveType;
    }
}