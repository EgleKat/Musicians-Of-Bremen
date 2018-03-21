using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	void Start () {
        EventManager.TriggerEvent(EventType.CreateBuildings, null);
	}
}
