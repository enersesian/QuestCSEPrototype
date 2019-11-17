using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Singleton class responsible for managing settings, references for recordings
/// </summary>
public class HCInvestigatorManager : MonoBehaviour
{

    /// <summary>
    /// Singleton variable
    /// </summary>
    public static HCInvestigatorManager instance;

    /// <summary>
    /// Max duration to record audio 
    /// </summary>
    public int audioRecordDuration;

    /// <summary>
    /// Folder name for screenshots which can be used to create a video
    /// </summary>
    public string folderName;

    /// <summary>
    /// Holds references to events made with HCInvestigatorEvent
    /// </summary>
    private Dictionary<string, UnityEvent> m_eventDict;

    /// <summary>
    /// Variable to tell whether or not text analytics data is currently being recorded
    /// </summary>
    [HideInInspector]
    public bool m_recordingTextData;

    /// <summary>
    /// Boolean that says whether or not audio is currently being recorded
    /// </summary>
    [HideInInspector]
    public bool m_recordingAudio;

    /// <summary>
    /// Boolean that says whether or not video is currently being recorded
    /// </summary>
    [HideInInspector]
    public bool m_recordingVideo;

    /// <summary>
    /// AndroidJavaObject that represents a screen recorder object
    /// </summary>
    private AndroidJavaObject m_ScreenRecorder;

    /// <summary>
    /// Used as a representation of the UnityPlayer Java class
    /// </summary>
    private AndroidJavaClass m_javaClass;

    /// <summary>
    /// The text data to write to a file
    /// </summary>
    //public string TextData { get; private set; }

    private List<string> TextData = new List<string>();

    /// <summary>
    /// Audio source used to record voice
    /// </summary>
    public AudioSource AudioSource { get; private set; }

    private string m_finalFolderName;
    private string textFileName;
    private List<bool> recordingText = new List<bool>();

    /// <summary>
    /// Used for initialization before the game starts
    /// </summary>
    void Awake()
    {
        if (!Application.isEditor)
        {
            if (instance == null)
            {
                instance = this;
                m_eventDict = new Dictionary<string, UnityEvent>();
                AudioSource = GetComponent<AudioSource>();
                m_javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                m_ScreenRecorder = m_javaClass.GetStatic<AndroidJavaObject>("currentActivity");
                m_ScreenRecorder.Call("setupVideo", 2000000, 30);
                m_finalFolderName = Application.persistentDataPath + "/" + folderName + " " + DateTime.Now.ToString("MM-dd-yy hh_mm_ss");
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        else gameObject.SetActive(false); //running in editor, turn off to stop errors as currently only works on android devices
        
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Sets up the video's name and starts recording the screen
    /// </summary>
    public void StartRecordingVideo()
    {
        if (m_recordingVideo)
        {
            return;
        }

        m_recordingVideo = true;
        string fileName = "Video";

        m_ScreenRecorder.Call("setFileName", m_finalFolderName, fileName);
        m_ScreenRecorder.Call("startRecording");
    }

    /// <summary>
    /// Stop recording video
    /// </summary>
    public void StopRecordingVideo()
    {
        if (!m_recordingVideo)
        {
            return;
        }

        m_recordingVideo = false;
        m_ScreenRecorder.Call("stopRecording");
    }



    /// <summary>
    /// Begins recording audio
    /// </summary>
    public void StartRecordingAudio()
    {
        if (m_recordingAudio)
        {
            return;
        }
        instance.m_recordingAudio = true;
        instance.AudioSource.clip = Microphone.Start(null, true, instance.audioRecordDuration, 44100);
        while (!(Microphone.GetPosition(null) > 0)) { }
    }

    /// <summary>
    /// Stops recording audio
    /// </summary>
    public void StopRecordingAudio()
    {
        if (!instance.m_recordingAudio)
        {
            return;
        }

        instance.m_recordingAudio = false;
        Microphone.End(null);
        SavWav.Save(m_finalFolderName + "/" + DateTime.Now.ToString("hh_mm_ss"), instance.AudioSource.clip);
    }

    /// <summary>
    /// Begins recording analytics as text data
    /// </summary>
    public void StartRecordingText(int index)
    {
        if (index == TextData.Count)
        {
            TextData.Add("");
            recordingText.Add(true);
        }

        if (index < recordingText.Count && !recordingText[index])
        {
            recordingText[index] = true;

            for (int i = 0; i < TextData.Count; i++)
            {
                TextData[i] = "";
            }
        }
    }

    /// <summary>
    /// Stops recording analytics as text data
    /// </summary>
    public void StopRecordingText(int index, string fileName)
    {
        if (index < recordingText.Count && !recordingText[index])
        {
            return;
        }

        recordingText[index] = false;
        StreamWriter writer = null;

        if (!System.IO.Directory.Exists(m_finalFolderName))
        {
            System.IO.Directory.CreateDirectory(m_finalFolderName);
        }

        if (System.IO.File.Exists(m_finalFolderName + "/" + fileName))
        {
            writer = System.IO.File.AppendText(m_finalFolderName + "/" + fileName);
        }
        else
        {
            writer = System.IO.File.CreateText(m_finalFolderName + "/" + fileName);
        }

        writer.WriteLine(TextData[index]);
        writer.Close();
    }


    /// <summary>
    /// Appends additional text to the current text data
    /// </summary>
    /// <param name="data">The text to append</param>
    public void WriteTextData(int index, string data)
    {
        if (index < recordingText.Count && !recordingText[index])
        {
            return;
        }

        if (index < TextData.Count && index > -1)
        {
            TextData[index] += data + Environment.NewLine;
        }
    }

    /// <summary>
    /// Adds an event to the event dictionary
    /// </summary>
    /// <param name="eventName">The name of the event</param>
    public void AddToDictionary(string eventName)
    {
        UnityEvent thisevent = null;
        if (instance.m_eventDict.ContainsKey(eventName))
        {
            return;
        }
        else
        {
            thisevent = new UnityEvent();
            instance.m_eventDict.Add(eventName, thisevent);
        }
    }

    /// <summary>
    /// Adds a listener to an event in the event dictionary
    /// </summary>
    /// <param name="eventName">The name of the event</param>
    /// <param name="listener">The listener to add to the event</param>
    public void AddActionToDictionary(string eventName, UnityAction listener)
    {
        if (!m_eventDict.ContainsKey(eventName))
        {
            Debug.LogError("Key doesnt exist in dictionary");
        }

        else
        {

            m_eventDict[eventName].AddListener(listener);
        }
    }

    /// <summary>
    /// Invokes a specific event
    /// </summary>
    /// <param name="eventName">The name of the event</param>
    public void InvokeEvent(string eventName)
    {
        if (!m_eventDict.ContainsKey(eventName))
        {
            Debug.LogError("Key doesnt exist in dictionary");
        }
        else
        {
            m_eventDict[eventName].Invoke();
        }
    }
}
