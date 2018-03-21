using UnityEngine.Events;

public enum EventType
{
    GoOutside,
    GoInside,
    CreateBuildings,
    PressedInteractKey,
    StartDialogueWith,
    EndDialogueWith,
}

[System.Serializable]
public class UnityEventWithObject : UnityEvent<object> { }