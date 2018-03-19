using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorDeactivateOnStart : MonoBehaviour {
	void Start () {
        gameObject.SetActive(false);
	}
}
