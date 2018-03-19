using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void GameObjectEventHandler(GameObject gO);
    public delegate void EmptyEventHandler();

    public EmptyEventHandler WentInside;
    public EmptyEventHandler WentOutside;
}
