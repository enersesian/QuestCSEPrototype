using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleVideoRecord : MonoBehaviour {
    private bool called;
    private float timer;

	// Use this for initialization
	void Start () {
        timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (!called)
        {
            HCInvestigatorManager.instance.StartRecordingVideo();
            called = true;
        }
        else if (Time.time - timer >= 30.0f)
        {
            HCInvestigatorManager.instance.StopRecordingVideo();
        }
	}
}
