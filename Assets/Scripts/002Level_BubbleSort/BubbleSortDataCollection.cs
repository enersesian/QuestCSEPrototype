using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortDataCollection : Listener
{

    //used for data collection
    [SerializeField]
    private Transform centerEyeAnchor, leftHandAnchor, rightHandAnchor;
    [SerializeField]
    private bool shouldRecordUserData;
    private float textRecordTimer;
    private string recordingValues;

    // Use this for initialization
    void Start()
    {
        //User data collection initialization
        if (shouldRecordUserData)
        {
            //Start recording text data, user started level and tutorial task 00
            HCInvestigatorManager.instance.StartRecordingText(0);
            HCInvestigatorManager.instance.StartRecordingText(1);
            HCInvestigatorManager.instance.WriteTextData(0, "-----------User started bubble sort level with tutorial at " + DateTime.Now.ToString("hh:mm:ss") + "------------");
            HCInvestigatorManager.instance.WriteTextData(1, "-----------User started bubble sort level with tutorial at " + DateTime.Now.ToString("hh:mm:ss") + "------------");
            HCInvestigatorManager.instance.WriteTextData(1, ",,Head Position,,,Head Rotation,,,Left Hand Position,,,Right Hand Position");
            HCInvestigatorManager.instance.WriteTextData(1, "Time, X, Y, Z, X, Y, Z, X, Y, Z, X, Y, Z, Task");
            HCInvestigatorManager.instance.StartRecordingVideo();
            textRecordTimer = Time.time;
        }
        if(!Application.isEditor)
        {
            Invoke("SetStationHeight", 0.5f);
            Invoke("SetStationHeight", 1f);
        }
    }

    /// <summary>
    /// Set height of stations based on height of user's head
    /// </summary>
    public void SetStationHeight()
    {
        transform.position = new Vector3(transform.position.x, centerEyeAnchor.position.y - 1.75f, transform.position.z);
    }

    private void Update()
    {
        //record head position/rotation and hand positions every 0.5 seconds
        if (Time.time - textRecordTimer >= 0.5f && shouldRecordUserData)
        {
            recordingValues = DateTime.Now.ToString("hh:mm:ss:ff") + "," +
                centerEyeAnchor.position.x.ToString("F3") + "," + centerEyeAnchor.position.y.ToString("F3") + "," + centerEyeAnchor.position.z.ToString("F3") + "," +
                centerEyeAnchor.rotation.eulerAngles.x.ToString("F3") + "," + centerEyeAnchor.rotation.eulerAngles.y.ToString("F3") + "," + centerEyeAnchor.rotation.eulerAngles.z.ToString("F3") + "," +
                leftHandAnchor.position.x.ToString("F3") + "," + leftHandAnchor.position.y.ToString("F3") + "," + leftHandAnchor.position.z.ToString("F3") + "," +
                rightHandAnchor.position.x.ToString("F3") + "," + rightHandAnchor.position.y.ToString("F3") + "," + rightHandAnchor.position.z.ToString("F3") + "," + level.currentState.ToString();

            HCInvestigatorManager.instance.WriteTextData(1, recordingValues);
            textRecordTimer = Time.time;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        //pause recordings if user temporary leaves app
        if (shouldRecordUserData)
        {
            if (pause)
            {
                HCInvestigatorManager.instance.StopRecordingText(0, "TextLog.txt");
                HCInvestigatorManager.instance.StopRecordingText(1, "PositionAndRotationLog.csv");
            }
            else
            {
                HCInvestigatorManager.instance.StartRecordingText(0);
                HCInvestigatorManager.instance.StartRecordingText(1);
            }
        }
    }

    public override void SetListenerState(BubbleSortState currentState)
    {
        if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, "User went to state " + level.currentState.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
    }

    public override void TaskSuccessful()
    {
        if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, "Task is successful at " + level.currentState.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
    }

    public override void TaskUnsuccessful(int hint)
    {
        if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, "Task is unsuccessful at container " + hint.ToString() + " at task " + level.currentState.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
    }

    public override void ButtonPushed(string buttonName)
    {
        if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, buttonName.ToString() + " was pushed at " + DateTime.Now.ToString("hh:mm:ss"));
    }
}
