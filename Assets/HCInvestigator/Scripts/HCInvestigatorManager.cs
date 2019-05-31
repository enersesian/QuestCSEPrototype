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
    /// Folder name for audio recordings
    /// </summary>
    public string audioFolderName;
    /// <summary>
    /// Max duration to record audio 
    /// </summary>
    public int audioRecordDuration;
    /// <summary>
    /// Folder name for screenshots which can be used to create a video
    /// </summary>
    public string videoFolderName;
    /// <summary>
    /// Folder name for text data recordings
    /// </summary>
    public string textFolderName;

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
    public string TextData { get; private set; }

    /// <summary>
    /// Audio source used to record voice
    /// </summary>
    public AudioSource AudioSource { get; private set; }

    /// <summary>
    /// Used for initialization before the game starts
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            m_eventDict = new Dictionary<string, UnityEvent>();
            AudioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        m_javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        m_ScreenRecorder = m_javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        m_ScreenRecorder.Call("setupVideo", 2000000, 30);
    }

    /// <summary>
    /// Sets up the video's name and starts recording the screen
    /// </summary>
    public void StartRecordingVideo()
    {
        if (!m_recordingVideo)
        {
            m_recordingVideo = true;
            string folderName = Application.persistentDataPath + "/" + videoFolderName + " " + DateTime.Now.ToString("MM-dd-yy hh_mm_ss");
            string fileName = DateTime.Now.ToString("hh_mm_ss");

            m_ScreenRecorder.Call("setFileName", folderName, fileName);
            m_ScreenRecorder.Call("startRecording");
        }
        else
        {
            m_recordingVideo = false;
            StopRecordingVideo();
        }
    }

    /// <summary>
    /// Stop recording video
    /// </summary>
    public void StopRecordingVideo()
    {
        m_ScreenRecorder.Call("stopRecording");
    }



    /// <summary>
    /// Begins recording audio
    /// </summary>
    public void StartRecordingAudio()
    {
        if (instance.m_recordingAudio)
        {
            instance.m_recordingAudio = false;
            Microphone.End(null);
            SavWav.Save(instance.audioFolderName +
            " " + DateTime.Now.ToString("MM-dd-yy hh_mm_ss") + "/" + DateTime.Now.ToString("hh_mm_ss"), instance.AudioSource.clip);
            return;
        }

        instance.m_recordingAudio = true;
        instance.AudioSource.clip = Microphone.Start(null, true, instance.audioRecordDuration, 44100);
        while (!(Microphone.GetPosition(null) > 0)) { }
    }

    /// <summary>
    /// Begins recording analytics as text data
    /// </summary>
    public void StartRecordingText()
    {
        if (!instance.m_recordingTextData)
        {
            instance.m_recordingTextData = true;
        }

    }

    /// <summary>
    /// Stops recording analytics as text data
    /// </summary>
    public void StopRecordingText()
    {
        if (!m_recordingTextData)
        {
            return;
        }

        instance.m_recordingTextData = false;
        string path = Application.persistentDataPath + "/" + instance.textFolderName + " " + DateTime.Now.ToString("MM-dd-yy hh_mm_ss");
        string fileName = DateTime.Now.ToString("hh_mm_ss") + ".txt";
        StreamWriter writer = null;
        if (System.IO.File.Exists(path + "/" + fileName))
        {
            writer = System.IO.File.AppendText(path + "/" + fileName);
        }
        else
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            writer = System.IO.File.CreateText(path + "/" + fileName);
        }

        writer.WriteLine(TextData);
        writer.Close();
    }


    /// <summary>
    /// Appends additional text to the current text data
    /// </summary>
    /// <param name="data">The text to append</param>
    public void WriteTextData(string data)
    {
        if (!m_recordingTextData)
        {
            return;
        }
        TextData += data + Environment.NewLine;
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
