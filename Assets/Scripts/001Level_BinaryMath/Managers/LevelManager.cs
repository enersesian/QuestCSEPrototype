using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    //keeping hitColor for now as we may reintroduce the buttons into the experience later
    [Tooltip("Active and disabled colors for levers")]
    public Color activeColor, disabledColor, hitColor;

    //refactor: create a station base class and turn these into singletons
    [Space(10)]
    public TaskStation taskStation;
    public NumberStation numberStation;
    public ColorStation colorStation;
    public ShapeStation shapeStation;
    public OutputStation outputStation;
    public TutorialStation tutorialStation;

    //used for data collection
    [SerializeField]
    private Transform centerEyeAnchor, leftHandAnchor, rightHandAnchor;
    [SerializeField]
    private bool shouldRecordUserData;

    //refactor: Convert task status and requirements from int arrays into enums
    private int currentTask;
    //0 = task, 1 = number bit 1, 2 = number bit 2, 3 = number bit 3, 4 = size bit 1, 5 = size bit 2
    //6 = color red bit, 7 = color green bit, 8 = color blue bit, possibly add shape later
    private int[] currentTaskStatus = new int[9];
    private int[] currentTaskRequirements = new int[9];
    private int currentNumber;
    private Color currentColor;
    private string currentColorText, currentShape, recordingValues;
    private bool isTutorialColorLeverPulled, isTutorialShapeLeverPulled, isTutorialRunOutputLeverPulled, isTutorialOutputSent;
    private float textRecordTimer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //User data collection initialization
        if (shouldRecordUserData)
        {
            //Start recording text data, user started level and tutorial task 00
            HCInvestigatorManager.instance.StartRecordingText(0);
            HCInvestigatorManager.instance.StartRecordingText(1);
            HCInvestigatorManager.instance.WriteTextData(0, "-----------User started level and tutorial task 00 at " + DateTime.Now.ToString("hh:mm:ss") + "------------");
            HCInvestigatorManager.instance.WriteTextData(1, "-----------User started level and tutorial task 00 at " + DateTime.Now.ToString("hh:mm:ss") + "------------");
            HCInvestigatorManager.instance.WriteTextData(1, ",,Head Position,,,Head Rotation,,,Left Hand Position,,,Right Hand Position");
            HCInvestigatorManager.instance.WriteTextData(1, "Time, X, Y, Z, X, Y, Z, X, Y, Z, X, Y, Z, Task");
            HCInvestigatorManager.instance.StartRecordingVideo();
            textRecordTimer = Time.time;
        }

        SetLevelScale();
        //set station height multiple times in beginning as dont know when headset passed to user
        Invoke("SetStationHeight", 0.5f); 
        Invoke("SetStationHeight", 1f);
        Invoke("SetStationHeight", 2f);

        //Begin the tutorial session of the experience
        tutorialStation.StartTutorialNonInteractive();
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
                rightHandAnchor.position.x.ToString("F3") + "," + rightHandAnchor.position.y.ToString("F3") + "," + rightHandAnchor.position.z.ToString("F3") + "," + currentTask.ToString();

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

    //Setters for user interactions with the number, shape, and color stations

    /// <summary>
    /// Set distance of play area, ie station distance, from 20'x20' to 12'x12' and vice versa
    /// </summary>
    public void SetLevelDistance(bool isNear)
    {
        tutorialStation.SetLevelDistance(isNear);
        taskStation.SetLevelDistance(isNear);
        numberStation.SetLevelDistance(isNear);
        colorStation.SetLevelDistance(isNear);
        shapeStation.SetLevelDistance(isNear);
        outputStation.SetLevelDistance(isNear);
    }

    /// <summary>
    /// Set height of stations based on height of user's head
    /// </summary>
    public void SetStationHeight()
    {
        transform.position = new Vector3(transform.position.x, centerEyeAnchor.position.y - 2, transform.position.z);
    }

    /// <summary>
    /// Turn on/off scaling effect on level based on user set boundary size
    /// </summary>
    public void SetLevelScale()
    {
        GetComponent<LevelScaling>().SetLevelScale();
    }

    /// <summary>
    /// Set new task for the level
    /// </summary>
    public void SetTask(string condition, int taskIterator)
    {
        currentNumber = 0;
        currentColor = new Color(0f, 0f, 0f);
        currentColorText = "Black";
        currentShape = "Cube";

        currentTask = taskIterator;
        for (int i = 0; i < currentTaskStatus.Length; i++)
        {
            currentTaskStatus[i] = 0;
            currentTaskRequirements[i] = 0;
        }

        taskStation.SetTask(condition);
        numberStation.SetTask();
        colorStation.SetTask();
        shapeStation.SetTask();
        outputStation.SetTask();
    }

    public void SetNumber(int bit, int bitStatus)
    {
        //recording user's interactions with the number station
        if (shouldRecordUserData)
        {
            if (bit == 1)
            {
                HCInvestigatorManager.instance.WriteTextData(0, "User set number lever 001 to " + bitStatus.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
            }
            else if (bit == 2)
            {
                HCInvestigatorManager.instance.WriteTextData(0, "User set number lever 010 to " + bitStatus.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
            }
            else
            {
                HCInvestigatorManager.instance.WriteTextData(0, "User set number lever 100 to " + bitStatus.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
            }
        }

        //prevents resetting bit to same bitStatus multiple times when user places lever in new position
        if (currentTaskStatus[bit] != bitStatus) 
        {
            currentTaskStatus[bit] = bitStatus;
            currentNumber = currentTaskStatus[1] + (2 * currentTaskStatus[2]) + (4 * currentTaskStatus[3]);

            numberStation.UpdateBitText(GetNumber());
            outputStation.UpdateNumText(GetNumber(), currentTask);
            //only need to call this once at first ever number lever pull, but its just setting same bool to true over and over so leave for now
            tutorialStation.TutorialNumberLeverIsPulled(); 
        }
    }

    public void SetColor(int bit, int bitStatus)
    {
        //recording user's interactions with the color station
        if (shouldRecordUserData)
        {
            if (bit == 4)
            {
                HCInvestigatorManager.instance.WriteTextData(0, "User set color lever 100 red to " + bitStatus.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
            }
            else if (bit == 5)
            {
                HCInvestigatorManager.instance.WriteTextData(0, "User set color lever 010 green to " + bitStatus.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
            }
            else
            {
                HCInvestigatorManager.instance.WriteTextData(0, "User set color lever 001 blue to " + bitStatus.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
            }
        }

        //prevents resetting bit to same bitStatus multiple times when user places lever in new position
        if (currentTaskStatus[bit] != bitStatus) 
        {
            currentTaskStatus[bit] = bitStatus;
            currentColor = new Color(currentTaskStatus[4], currentTaskStatus[5], currentTaskStatus[6]);

            if (currentColor.r == 1 && currentColor.b == 1 && currentColor.g == 1) currentColorText = "White";
            if (currentColor.r == 1 && currentColor.b == 1 && currentColor.g == 0) currentColorText = "Purple";
            if (currentColor.r == 1 && currentColor.b == 0 && currentColor.g == 0) currentColorText = "Red";
            if (currentColor.r == 1 && currentColor.b == 0 && currentColor.g == 1) currentColorText = "Yellow";
            if (currentColor.r == 0 && currentColor.b == 1 && currentColor.g == 1) currentColorText = "Cyan";
            if (currentColor.r == 0 && currentColor.b == 0 && currentColor.g == 1) currentColorText = "Green";
            if (currentColor.r == 0 && currentColor.b == 1 && currentColor.g == 0) currentColorText = "Blue";
            if (currentColor.r == 0 && currentColor.b == 0 && currentColor.g == 0) currentColorText = "Black";

            colorStation.UpdateColorText(currentColor, currentColorText);
            outputStation.UpdateColorText(currentColor, currentColorText);
            if (!isTutorialColorLeverPulled)
            {
                isTutorialColorLeverPulled = true;
                tutorialStation.TutorialColorLeverIsPulled();
            }
        }
    }

    public void SetShape(int bit, int bitStatus)
    {
        //recording user's interactions with the shape station
        if (shouldRecordUserData)
        {
            if (bit == 7)
            {
                HCInvestigatorManager.instance.WriteTextData(0, "User set shape lever 01 to " + bitStatus.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
            }
            else
            {
                HCInvestigatorManager.instance.WriteTextData(0, "User set shape lever 10 to " + bitStatus.ToString() + " at " + DateTime.Now.ToString("hh:mm:ss"));
            }
        }

        //prevents resetting bit to same bitStatus multiple times when user places lever in new position
        if (currentTaskStatus[bit] != bitStatus) 
        {
            currentTaskStatus[bit] = bitStatus;
            if (currentTaskStatus[7] == 0 && currentTaskStatus[8] == 0) currentShape = "Cube";
            if (currentTaskStatus[7] == 0 && currentTaskStatus[8] == 1) currentShape = "Cone";
            if (currentTaskStatus[7] == 1 && currentTaskStatus[8] == 0) currentShape = "Sphere";
            if (currentTaskStatus[7] == 1 && currentTaskStatus[8] == 1) currentShape = "Ring";

            shapeStation.UpdateShapeText(currentShape);
            outputStation.UpdateShapeText(currentShape);
            if (!isTutorialShapeLeverPulled)
            {
                isTutorialShapeLeverPulled = true;
                tutorialStation.TutorialShapeLeverIsPulled();
            }
        }
    }

    //Getters for current state of task

    public int GetNumber()
    {
        return currentNumber;
    }

    public Color GetColor()
    {
        return currentColor;
    }

    public string GetColorText()
    {
        return currentColorText;
    }

    public string GetShape()
    {
        return currentShape;
    }

    //Task event handlers

    //initialize task since pulling the get task lever starts off each task
    public void GetTaskLeverPulled(string inputText) 
    {
        if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, inputText + DateTime.Now.ToString("hh:mm:ss"));

        switch (currentTask)
        {
            case 1:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 1; //number = 1
                currentTaskRequirements[2] = 0;
                currentTaskRequirements[3] = 0;
                currentTaskRequirements[4] = 1; //color = red
                currentTaskRequirements[5] = 0;
                currentTaskRequirements[6] = 0;
                currentTaskRequirements[7] = 1; //shape = sphere
                currentTaskRequirements[8] = 0;
                break;

            case 2:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 0; //number = 2
                currentTaskRequirements[2] = 1;
                currentTaskRequirements[3] = 0;
                currentTaskRequirements[4] = 1; //color = red
                currentTaskRequirements[5] = 0;
                currentTaskRequirements[6] = 0;
                currentTaskRequirements[7] = 1; //shape = sphere
                currentTaskRequirements[8] = 0;
                break;

            case 3:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 0; //number = 4
                currentTaskRequirements[2] = 0;
                currentTaskRequirements[3] = 1;
                currentTaskRequirements[4] = 0; //color = green
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 0;
                currentTaskRequirements[7] = 0; //shape = cube
                currentTaskRequirements[8] = 0;
                break;

            case 4:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 1; //number = 3
                currentTaskRequirements[2] = 1;
                currentTaskRequirements[3] = 0;
                currentTaskRequirements[4] = 0; //color = blue
                currentTaskRequirements[5] = 0;
                currentTaskRequirements[6] = 1;
                currentTaskRequirements[7] = 0; //shape = cone
                currentTaskRequirements[8] = 1;
                break;

            case 5:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 1; //number = 5
                currentTaskRequirements[2] = 0;
                currentTaskRequirements[3] = 1;
                currentTaskRequirements[4] = 1; //color = yellow
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 0;
                currentTaskRequirements[7] = 1; //shape = ring
                currentTaskRequirements[8] = 1;
                break;

            case 6:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 1; //number = 7
                currentTaskRequirements[2] = 1;
                currentTaskRequirements[3] = 1;
                currentTaskRequirements[4] = 0; //color = cyan
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 1;
                currentTaskRequirements[7] = 0; //shape = cone
                currentTaskRequirements[8] = 1;
                break;

            case 7:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 0; //number = 6
                currentTaskRequirements[2] = 1;
                currentTaskRequirements[3] = 1;
                currentTaskRequirements[4] = 1; //color = purple
                currentTaskRequirements[5] = 0;
                currentTaskRequirements[6] = 1;
                currentTaskRequirements[7] = 1; //shape = ring
                currentTaskRequirements[8] = 1;
                break;

            default:
                break;
        }

        taskStation.GetTaskLeverPulled(currentTask);
        numberStation.GetTaskLeverPulled(currentTask);
        colorStation.GetTaskLeverPulled(currentTask);
        shapeStation.GetTaskLeverPulled(currentTask);
        colorStation.GetTaskLeverPulled(currentTask);
        outputStation.GetTaskLeverPulled(currentTask);
        tutorialStation.GetTaskLeverPulled(currentTask);
    }

    public void RunOutputLeverPulled(string inputText)
    {
        if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, inputText + DateTime.Now.ToString("hh:mm:ss"));

        numberStation.RunOutputLeverPulled();
        colorStation.RunOutputLeverPulled();
        shapeStation.RunOutputLeverPulled();
        outputStation.RunOutputLeverPulled(GetNumber(), currentColor, currentShape);
        if (!isTutorialRunOutputLeverPulled)
        {
            isTutorialRunOutputLeverPulled = true;
            tutorialStation.TutorialRunOutputLeverIsPulled();
        }
    }

    public void RunOutput()
    {
        taskStation.RunOutput();
    }

    public void PlacedOutput()
    {
        taskStation.PlacedOutput();
        tutorialStation.TutorialContainerPlacedOutput();
    }

    public void RemovedOutput()
    {
        taskStation.RemovedOutput();
    }

    public void OutputContainerGrabbed()
    {
        tutorialStation.TutorialContainerPickedUp();
    }

    //user ends task, success or failure resets stations for repeat of task or give new task
    public void OutputSent(string inputText) 
    {
        if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, inputText + DateTime.Now.ToString("hh:mm:ss"));

        int number = GetNumber();
        outputStation.OutputSent(number);
        if (currentTaskStatus[1] == currentTaskRequirements[1] && currentTaskStatus[2] == currentTaskRequirements[2] &&
            currentTaskStatus[3] == currentTaskRequirements[3] && currentTaskStatus[4] == currentTaskRequirements[4] &&
            currentTaskStatus[5] == currentTaskRequirements[5] && currentTaskStatus[6] == currentTaskRequirements[6] &&
            currentTaskStatus[7] == currentTaskRequirements[7] && currentTaskStatus[8] == currentTaskRequirements[8])
        {
            //finished last task and end the game
            if (currentTask == 7)
            {

                if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, "Task " + currentTask + " successful - " + DateTime.Now.ToString("hh:mm:ss"));
                if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, "Level complete - " + DateTime.Now.ToString("hh:mm:ss"));

                LevelComplete();
            }
            //finished a task, give next task
            else
            {
                if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, "Task " + currentTask + " successful - " + DateTime.Now.ToString("hh:mm:ss"));

                SetTask("success", currentTask + 1);
            }
        }

        else
        {
            if (shouldRecordUserData) HCInvestigatorManager.instance.WriteTextData(0, "Task " + currentTask + " failed - " + DateTime.Now.ToString("hh:mm:ss"));

            SetTask("failure", currentTask);
        }
        if (!isTutorialOutputSent)
        {
            isTutorialOutputSent = true;
            tutorialStation.TutorialOutputSent();
        }
    }

    public void TutorialLeverPulled(bool isTutorialLeverOn)
    {
        if (shouldRecordUserData)
        {
            if (isTutorialLeverOn)
            {
                HCInvestigatorManager.instance.WriteTextData(0, "User set tutorial lever to one at " + DateTime.Now.ToString("hh:mm:ss"));
            }
            else
            {
                HCInvestigatorManager.instance.WriteTextData(0, "User set tutorial lever to zero at " + DateTime.Now.ToString("hh:mm:ss"));
            }
        }
            
        tutorialStation.SetTutorialLeverBool(isTutorialLeverOn);
    }

    public void LevelComplete()
    {
        //Disable everything
        taskStation.LevelComplete();
    }
}
