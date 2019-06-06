using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class NodeReachedEvent : HCInvestigatorEvent {

    private UnityAction listner;
	// Use this for initialization
	void Start () {

        listner = new UnityAction(NodeReached);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void NodeReached()
    {
        HCInvestigatorManager.instance.WriteTextData(0, "A node has been hit at" + DateTime.Now.ToString("MM-dd-yy hh_mm_ss"));
    }
}
