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
    FadeIn,
    FadeOut,
    EndFadeOut,
    EndFadeIn,
    ChangeMusic,
    PlaySound,
    StartInteraction,
    EndInteraction,
    TriggerCollide,
    EndTriggerCollide,
    StartSimonSaysRound,
    StartSimonSaysRecall,
    StartAlertSimonSays,
    EndAlertSimonSays,
    EndSimonSays,
    ChangeLeader,
    RemoveHeart,
}

[System.Serializable]
public class UnityEventWithObject : UnityEvent<object> { }

public class MoveCommand
{
    public enum MoveType { Direction, Location }

    public string gameObjectToMove;
    public Vector3? vec;
    public MoveType moveType;


    public MoveCommand(string gameObjectToMove, Vector3? vec, MoveType moveType)
    {
        this.gameObjectToMove = gameObjectToMove;
        this.vec = vec;
        this.moveType = moveType;
    }

    public override string ToString()
    {
        return "{" + gameObjectToMove + "," + vec + "," + moveType + "}";
    }
}
public class FadeCommand
{
    public string gameObjectToFade;
    public float timeToFade;

    public FadeCommand(string gameObjectToFade, float timeToFade)
    {
        this.gameObjectToFade = gameObjectToFade;
        this.timeToFade = timeToFade;
    }
}
