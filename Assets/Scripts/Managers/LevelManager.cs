using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public TaskStation taskStation;
    public NumberStation numberStation;
    public ColorStation colorStation;
    public ShapeStation shapeStation;
    public OutputStation outputStation;
    public TutorialStation tutorialStation;

    public Color activeColor, disabledColor, hitColor, red, green, blue;
    public OVRGrabber leftHandGrabber, rightHandGrabber;
    public Transform headsetCenterAnchor;

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

    void Start ()
    {
        Invoke("SetUserHeight", 0.5f); //set back to 5 seconds to take off researcher head and onto subject head for height adjustment
        tutorialStation.StartTutorial();
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

    private void SetUserHeight()
    {
        transform.position = new Vector3(transform.position.x, headsetCenterAnchor.position.y, transform.position.z);
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

    /*
    public void InteractableActivacted(string name, int action)
    {
        if (name == "lever0001") SetNumber(1, action); //number bit 1 = action
        if (name == "lever0010") SetNumber(2, action); //number bit 2 = action
        if (name == "lever0100") SetNumber(3, action); //number bit 3 = action
        //if (transform.parent.name == "lever1000") SetNumber(4, action); //if I return to 4 bit numbers
        if (name == "lever01") SetSize(4, action); //size bit 1 = action
        if (name == "lever10") SetSize(5, action); //size bit 2 = action

        //lever pulled to up to onePosition
        if (name == "leverGetTask") GetTaskLeverPulled();
        if (name == "leverRunOutput") RunOutputLeverPulled();
        if (name == "leverSendOutput") SendOutputLeverPulled();
    }
    */
    public void ForceGrabberRelease(OVRGrabbable grabbable)
    {
        if (grabbable != null)
        {
            leftHandGrabber.ForceRelease(grabbable);
            rightHandGrabber.ForceRelease(grabbable);
        }
    }

    public void GetTaskLeverPulled()
    {
        switch(currentTask)
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

    public void RunOutputButtonPushed()
    {
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

    public void OutputSent()
    {
        int number = GetNumber();
        outputStation.OutputSent(number);
        if (currentTaskStatus[1] == currentTaskRequirements[1] && currentTaskStatus[2] == currentTaskRequirements[2] && 
            currentTaskStatus[3] == currentTaskRequirements[3] && currentTaskStatus[4] == currentTaskRequirements[4] && 
            currentTaskStatus[5] == currentTaskRequirements[5] && currentTaskStatus[6] == currentTaskRequirements[6] &&
            currentTaskStatus[7] == currentTaskRequirements[7] && currentTaskStatus[8] == currentTaskRequirements[8])
        {
            if(currentTask == 7) LevelComplete();
            else SetTask("success", currentTask + 1);
        }
        else
        {
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
        tutorialStation.SetTutorialLeverBool(isTutorialLeverOn);
    }

    public void LevelComplete()
    {
        //Disable everything
        taskStation.LevelComplete();
    }

    public void SetNumber(int bit, int bitStatus)
    {
        if(currentTaskStatus[bit] != bitStatus) //prevents resetting bit to same bitStatus multiple times when user places lever in new position
        {
            currentTaskStatus[bit] = bitStatus;
            currentNumber = currentTaskStatus[1] + (2 * currentTaskStatus[2]) + (4 * currentTaskStatus[3]);

            numberStation.UpdateBitText(bit, bitStatus);
            outputStation.UpdateNumText(GetNumber());
            if(!isTutorialNumberLeverPulled)
            {
                isTutorialNumberLeverPulled = true;
                tutorialStation.TutorialNumberLeverIsPulled();
            }

        }
        
    }

    public void SetColor(int bit, int bitStatus)
    {
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
}
