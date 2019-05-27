using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceHolderEvent : HCInvestigatorEvent {

    private UnityAction listner;
	// Use this for initialization
	void Start () {

        listner = new UnityAction(SomeFunction);
        RegisterToManager(listner);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SomeFunction()
    {
        Debug.Log("Some Function");
    }
}
