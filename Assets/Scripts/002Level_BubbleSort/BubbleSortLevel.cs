using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortLevel : MonoBehaviour {

    private List<Listener> listeners = new List<Listener>();
    public BubbleSortState currentState;
    public float resetTimer = 2f;

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
        //SetAppState(BubbleSortState.IntroductionToNextButton);
        SetAppState(currentState);

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
                rightHandAnchor.position.x.ToString("F3") + "," + rightHandAnchor.position.y.ToString("F3") + "," + rightHandAnchor.position.z.ToString("F3") + "," + currentState.ToString();

            HCInvestigatorManager.instance.WriteTextData(1, recordingValues);
            textRecordTimer = Time.time;
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1)) SetAppState(BubbleSortState.IntroductionToNextButton);
        //if (Input.GetKeyDown(KeyCode.Alpha2)) SetAppState(BubbleSortState.BeginnerBubbleSortTask01);
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

    public void RegisterListener(Listener newListener)
    {
        listeners.Add(newListener);
        Debug.Log("Registered " + newListener.gameObject.name);
    }

    private void SetAppState(BubbleSortState tempState)
    {
        if(shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, "User went to state " + tempState.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
        currentState = tempState;
        Debug.Log(currentState.ToString());
        foreach (Listener listenerObj in listeners) listenerObj.SetListenerState(tempState);
    }

    public void TaskSuccessful()
    {
        foreach (Listener listenerObj in listeners) listenerObj.TaskSuccessful();
        switch (currentState)
        {
            case BubbleSortState.BeginnerBubbleSortTask01:
                SetAppState(BubbleSortState.BeginnerBubbleSortTask01Complete);
                break;

            case BubbleSortState.BeginnerBubbleSortTask02:
                SetAppState(BubbleSortState.BeginnerBubbleSortTask02Complete);
                break;

            case BubbleSortState.BeginnerBubbleSortTask03:
                SetAppState(BubbleSortState.BeginnerBubbleSortTask03Complete);
                break;

            case BubbleSortState.BeginnerBubbleSortTask04:
                SetAppState(BubbleSortState.BeginnerBubbleSortTask04Complete);
                break;

            case BubbleSortState.BeginnerBubbleSortTask05:
                SetAppState(BubbleSortState.BeginnerBubbleSortTask05Complete);
                break;

            case BubbleSortState.BeginnerBubbleSortTask06:
                SetAppState(BubbleSortState.BeginnerBubbleSortTask06Complete);
                break;
        }
    }

    public void TaskUnsuccessful(int hint)
    {
        foreach (Listener listenerObj in listeners) listenerObj.TaskUnsuccessful(hint);
    }

    private void SetAppStateToIntroductionToCyclingThroughList()
    {
        SetAppState(BubbleSortState.IntroductionToCyclingThroughList);
    }

    private void SetAppStateToIntroductionToThreeElementList03()
    {
        SetAppState(BubbleSortState.IntroductionToThreeElementList03);
    }

    private void SetAppStateToIntroductionToThreeElementList05()
    {
        SetAppState(BubbleSortState.IntroductionToThreeElementList05);
    }

    public void PopulateContainer(int containerPosition, int containerContents)
    {
        foreach (Listener listenerObj in listeners) listenerObj.PopulateContainer(containerPosition, containerContents);
    }

    public void DepopulateContainer(int containerPosition)
    {
        foreach (Listener listenerObj in listeners) listenerObj.DepopulateContainer(containerPosition);
    }

    public void ButtonPushed(string buttonName)
    {
        switch (currentState)
        {
            case BubbleSortState.IntroductionToNextButton:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToSwapButton);
                break;

            case BubbleSortState.IntroductionToSwapButton:
                if (buttonName == "ButtonSwap")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    Invoke("SetAppStateToIntroductionToCyclingThroughList", resetTimer);
                }
                break;

            case BubbleSortState.IntroductionToCyclingThroughList:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToFinishingBubbleSort);
                break;

            case BubbleSortState.IntroductionToFinishingBubbleSort:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToThreeElementList01);
                break;

            case BubbleSortState.IntroductionToThreeElementList01:
                if (buttonName == "ButtonNext")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    SetAppState(BubbleSortState.IntroductionToThreeElementList02);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList02:
                if (buttonName == "ButtonSwap")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    Invoke("SetAppStateToIntroductionToThreeElementList03", resetTimer);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList03:
                if (buttonName == "ButtonNext")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    SetAppState(BubbleSortState.IntroductionToThreeElementList04);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList04:
                if (buttonName == "ButtonSwap")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    Invoke("SetAppStateToIntroductionToThreeElementList05", resetTimer);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList05:
                if (buttonName == "ButtonNext")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    SetAppState(BubbleSortState.IntroductionToThreeElementList06);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList06:
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToThreeElementList07);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList07:
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToThreeElementList08);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList08:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToThreeElementList09);
                break;

            case BubbleSortState.IntroductionToThreeElementList09:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.BeginnerBubbleSortTask01);
                break;

            case BubbleSortState.BeginnerBubbleSortTask01Complete:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.BeginnerBubbleSortTask02);
                break;

            case BubbleSortState.BeginnerBubbleSortTask02Complete:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.BeginnerBubbleSortTask03);
                break;

            case BubbleSortState.BeginnerBubbleSortTask03Complete:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.BeginnerBubbleSortTask04);
                break;

            case BubbleSortState.BeginnerBubbleSortTask04Complete:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.BeginnerBubbleSortTask05);
                break;

            case BubbleSortState.BeginnerBubbleSortTask05Complete:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.BeginnerBubbleSortTask06);
                break;

            default:
                Debug.Log("button pushed " + buttonName);
                foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                break;
        }
    }
}
