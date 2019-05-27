using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Contains data and functions for creating a UI button 
/// </summary>
public class HCInvestigatorUIButton : Button, HCInvestigatorInterface
{
    /// <summary>
    /// Folder name to save a recording to
    /// </summary>
    private string m_folderName;

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
    }

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
    public  void RecordTextData()
    {
        HCInvestigatorManager.instance.StartRecordingText();
    }
    /// <summary>
    /// Called whenever the button is clicked
    /// </summary>
    public void InvokeEvent()
    {
        onClick.Invoke();
    }
}
