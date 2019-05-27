using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles user interface with the recording system
/// </summary>
public interface HCInvestigatorInterface 
{
    /// <summary>
    /// Begins recording video if it is not currently being recorded
    /// </summary>
    void RecordVideo();
   
    /// <summary>
    /// Begins recording audio
    /// </summary>
    void RecordAudio();
   
    /// <summary>
    /// Begins recording analytics as text data
    /// </summary>
    void RecordTextData();
}
