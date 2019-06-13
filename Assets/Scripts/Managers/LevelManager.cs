using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Singleton variable
    /// </summary>
    public static LevelManager instance;

    public TaskStation taskStation;
    public NumberStation numberStation;
    public ColorStation colorStation;
    public ShapeStation shapeStation;
    public OutputStation outputStation;
    public TutorialStation tutorialStation;

    public Color activeColor, disabledColor, hitColor;
    [SerializeField]
    private Transform centerEyeAnchor, leftHandAnchor, rightHandAnchor;

    private int currentTask;
    //0 = task, 1 = number bit 1, 2 = number bit 2, 3 = number bit 3, 4 = size bit 1, 5 = size bit 2
    //6 = color red bit, 7 = color green bit, 8 = color blue bit, possibly add shape later
    private int[] currentTaskStatus = new int[9];
    private int[] currentTaskRequirements = new int[9];
    private int currentNumber;
    private Color currentColor;
    private string currentColorText;
    private string currentShape;
    private bool isTutorialNumberLeverPulled, isTutorialColorLeverPulled, isTutorialShapeLeverPulled, isTutorialRunOutputLeverPulled, isTutorialOutputSent;
    private string recordingValues;
    private float textRecordTimer;

    /// <summary>
    /// Used for initialization before the game starts
    /// </summary>
    void Awake()
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

    void Start()
    {
        //Invoke("SetUserHeight", 0.5f); //set back to 5 seconds to take off researcher head and onto subject head for height adjustment
        tutorialStation.StartTutorial();
       
        if (!Application.isEditor)
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
    }

    private void Update()
    {
        if (Time.time - textRecordTimer >= 0.5f && !Application.isEditor)
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

    public void SetLevelDistance(bool isNear)
    {
        tutorialStation.SetLevelDistance(isNear);
        taskStation.SetLevelDistance(isNear);
        numberStation.SetLevelDistance(isNear);
        colorStation.SetLevelDistance(isNear);
        shapeStation.SetLevelDistance(isNear);
        outputStation.SetLevelDistance(isNear);
    }

    public void SetStationHeight()
    {
        transform.position = new Vector3(transform.position.x, centerEyeAnchor.position.y, transform.position.z);
    }

    public void StartLevel()
    {
        SetTask("start", 1);
    }

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

    public void GetTaskLeverPulled(string inputText) // start task message
    {
        if (!Application.isEditor) HCInvestigatorManager.instance.WriteTextData(0, inputText + DateTime.Now.ToString("hh:mm:ss"));

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

    public void RunOutputButtonPushed(string inputText)
    {
        if (!Application.isEditor) HCInvestigatorManager.instance.WriteTextData(0, inputText + DateTime.Now.ToString("hh:mm:ss"));

        numberStation.RunOutputButtonPushed();
        colorStation.RunOutputButtonPushed();
        shapeStation.RunOutputButtonPushed();
        outputStation.RunOutputButtonPushed(GetNumber(), currentColor, currentShape);
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

    public void OutputSent(string inputText) //end of a task
    {
        if (!Application.isEditor) HCInvestigatorManager.instance.WriteTextData(0, inputText + DateTime.Now.ToString("hh:mm:ss"));

        int number = GetNumber();
        outputStation.OutputSent(number);
        if (currentTaskStatus[1] == currentTaskRequirements[1] && currentTaskStatus[2] == currentTaskRequirements[2] &&
            currentTaskStatus[3] == currentTaskRequirements[3] && currentTaskStatus[4] == currentTaskRequirements[4] &&
            currentTaskStatus[5] == currentTaskRequirements[5] && currentTaskStatus[6] == currentTaskRequirements[6] &&
            currentTaskStatus[7] == currentTaskRequirements[7] && currentTaskStatus[8] == currentTaskRequirements[8])
        {
            if (currentTask == 7)
            {

                if (!Application.isEditor) HCInvestigatorManager.instance.WriteTextData(0, "Task " + currentTask + " successful - " + DateTime.Now.ToString("hh:mm:ss"));
                if (!Application.isEditor) HCInvestigatorManager.instance.WriteTextData(0, "Level complete - " + DateTime.Now.ToString("hh:mm:ss"));

                LevelComplete();
            }
            else
            {
                if (!Application.isEditor) HCInvestigatorManager.instance.WriteTextData(0, "Task " + currentTask + " successful - " + DateTime.Now.ToString("hh:mm:ss"));

                SetTask("success", currentTask + 1);
            }
        }
        else
        {
            if (!Application.isEditor) HCInvestigatorManager.instance.WriteTextData(0, "Task " + currentTask + " failed - " + DateTime.Now.ToString("hh:mm:ss"));
            SetTask("failure", currentTask);
        }
        if (!isTutorialOutputSent)
        {
            isTutorialOutputSent = true;
            tutorialStation.TutorialOutputSent();
        }

        /*switch (currentTask)
        {
            case 1:
                if (number == 1) SetTask("success", 2); //user successful, iterate currentTask
                else SetTask("failure", 1); //user failed, keep currentTask
                break;

            case 2:
                if (number == 2) SetTask("success", 3);
                else SetTask("failure", 2); //user failed, keep currentTask
                break;

            case 3:
                if (number == 4) SetTask("success", 4);
                else SetTask("failure", 3); //user failed, keep currentTask
                break;

            case 4:
                if (number == 8) LevelComplete();
                else SetTask("failure", 4); //user failed, keep currentTask
                break;

            default:
                break;
        }*/
    }

    public void TutorialLeverPulled(bool isTutorialLeverOn)
    {
        if (!Application.isEditor)
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

    public void SetNumber(int bit, int bitStatus)
    {
        if (!Application.isEditor)
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
            


        if (currentTaskStatus[bit] != bitStatus) //prevents resetting bit to same bitStatus multiple times when user places lever in new position
        {
            currentTaskStatus[bit] = bitStatus;
            currentNumber = currentTaskStatus[1] + (2 * currentTaskStatus[2]) + (4 * currentTaskStatus[3]);

            numberStation.UpdateBitText(bit, bitStatus);
            outputStation.UpdateNumText(GetNumber());
            if (!isTutorialNumberLeverPulled)
            {
                isTutorialNumberLeverPulled = true;
                tutorialStation.TutorialNumberLeverIsPulled();
            }
        }
    }

    public void SetColor(int bit, int bitStatus)
    {
        if (!Application.isEditor)
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
            

        if (currentTaskStatus[bit] != bitStatus) //prevents resetting bit to same bitStatus multiple times when user places lever in new position
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
        if (!Application.isEditor)
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
            

        if (currentTaskStatus[bit] != bitStatus) //prevents resetting bit to same bitStatus multiple times when user places lever in new position
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

    private void OnApplicationPause(bool pause)
    {
        if (!Application.isEditor)
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
}
