using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Component used for setting physcial button pushes to record a certain type of data when pressed
/// </summary>
public class HCInvestigatorPhysicalButton : MonoBehaviour , HCInvestigatorInterface
{
    /// <summary>
    /// enum for which button usage we are looking for
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// ButtonType == Trigger
        /// </summary>
        Trigger,
        /// <summary>
        /// ButtonType == TouchPad
        /// </summary>
        TouchPad
    }
    
    /// <summary>
    /// Variable that holds button type of this physical button
    /// </summary>
    public ButtonType buttonType;
    
    /// <summary>
    /// UnityEvent that is called when the relevant button is pressed
    /// </summary>
    public UnityEvent onClick;

    /// <summary>
    /// Folder name to save a recording to
    /// </summary>
    private string m_folderName;

    /// <summary>
    /// Begins recording video if it is not currently being recorded
    /// </summary>
    public void RecordVideo()
    {
        if (!HCInvestigatorManager.instance.m_recordingVideo)
        {
            HCInvestigatorManager.instance.StartRecordingVideo();
        }
        else
        {
            HCInvestigatorManager.instance.StopRecordingVideo();
        }
    }

    /// <summary>
    /// Begins recording audio
    /// </summary>
    public void RecordAudio()
    {
        HCInvestigatorManager.instance.StartRecordingAudio();
    }

    /// <summary>
    /// Begins recording analytics as text data
    /// </summary>
    public void RecordTextData()
    {
        HCInvestigatorManager.instance.StartRecordingText(0);
    }
    
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //Checks to see if the player has hit the trigger and checks to see if the trigger is the desired button
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && buttonType == ButtonType.Trigger)
        {
           
            onClick.Invoke();
        }

        //Checks to see if the player has hit the touchpad and checks to see if the touchpad is the desired button
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) && buttonType == ButtonType.TouchPad)
        {
            onClick.Invoke();
        }
    }
}

