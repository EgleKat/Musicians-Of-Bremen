using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorRegisterEvents : MonoBehaviour {

    private EventManager eventManager;

	void Awake () {
        eventManager = GameObject.Find("Game Controller").GetComponent<EventManager>();
        eventManager.WentOutside += OnWentOutside;
        eventManager.WentInside += OnWentInside;
	}

    void OnWentOutside()
    {
        gameObject.SetActive(true);
    }
    void OnWentInside()
    {
        gameObject.SetActive(false);
    }
}
