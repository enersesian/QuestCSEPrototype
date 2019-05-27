using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatusUpdateEvent : MonoBehaviour {

    private UnityAction listner;
	// Use this for initialization
	void Start () {
        listner = new UnityAction(CheckProgress);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckProgress()
    {
        //probbaly write to a text log what node the user is on and how long the level has been up for to se how long it takes them to finish level  or parts of level
        //probably call this event every couple of mins or 5 mins
    }
}
