using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch_Interact : MonoBehaviour {

    bool inTrigger = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inTrigger)
            Debug.Log("E key was pressed");

    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger");
        inTrigger = true;

    }
    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

}
