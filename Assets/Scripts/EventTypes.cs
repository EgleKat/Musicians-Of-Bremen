using UnityEngine.Events;

public enum EventType
{
    GoOutside,
    GoInside,
    CreateBuildings,
}

[System.Serializable]
public class UnityEventWithObject : UnityEvent<object> { }