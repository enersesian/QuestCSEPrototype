using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public TaskStation taskStation;
    public NumberStation numberStation;
    public OutputStation outputStation;
    public SizeStation sizeStation;

    public Color activeColor, disabledColor, hitColor;
    public OVRGrabber leftHandGrabber, rightHandGrabber;

    private enum UserProgression { SetTask, getTask, runOutput };
    private UserProgression userProgression;

    private int currentTask;
    //0 = task, 1 = number bit 1, 2 = number bit 2, 3 = number bit 3, 4 = size bit 1, 5 = size bit 2
    //6 = color red bit, 7 = color green bit, 8 = color blue bit, possibly add shape later
    private int[] currentTaskStatus = new int[9]; 
    private int[] currentTaskRequirements = new int[9];
    private Color currentColor;
    private string size;

    void Start ()
    {
        SetLevel();
        SetTask("start", 3);
	}

    public void SetLevel()
    {
        userProgression = UserProgression.SetTask;
    }

    public void SetTask(string condition, int taskIterator)
    {
        currentTask = taskIterator;
        for (int i = 0; i < currentTaskStatus.Length; i++)
        {
            currentTaskStatus[i] = 0;
            currentTaskRequirements[i] = 0;
        }
        currentColor = new Color(1f, 0f, 0f);
        taskStation.SetTask(condition);
        numberStation.SetTask();
        sizeStation.SetTask();
        outputStation.SetTask();
    }

    public void ForceGrabberRelease(OVRGrabbable grabbable)
    {
        if (grabbable != null)
        {
            leftHandGrabber.ForceRelease(grabbable);
            rightHandGrabber.ForceRelease(grabbable);
        }
    }

    public void GetTaskButtonPushed()
    {
        userProgression = UserProgression.getTask;
        switch(currentTask)
        {
            case 1:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 1; //number = 1
                currentTaskRequirements[2] = 0;
                currentTaskRequirements[3] = 0;
                currentTaskRequirements[4] = 0; //size = small
                currentTaskRequirements[5] = 0; 
                currentTaskRequirements[6] = 1; //color = red
                currentTaskRequirements[7] = 0;
                currentTaskRequirements[8] = 0;
                break;

            case 2:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 0; //number = 2
                currentTaskRequirements[2] = 1;
                currentTaskRequirements[3] = 0;
                currentTaskRequirements[4] = 0; //size = small
                currentTaskRequirements[5] = 0;
                currentTaskRequirements[6] = 1; //color = red
                currentTaskRequirements[7] = 0;
                currentTaskRequirements[8] = 0;
                break;

            case 3:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 0; //number = 4
                currentTaskRequirements[2] = 0;
                currentTaskRequirements[3] = 1;
                currentTaskRequirements[4] = 0; //size = medium
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 1; //color = red
                currentTaskRequirements[7] = 0;
                currentTaskRequirements[8] = 0;
                break;

            case 4:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 1; //number = 3
                currentTaskRequirements[2] = 1;
                currentTaskRequirements[3] = 0;
                currentTaskRequirements[4] = 0; //size = medium
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 0; //color = green
                currentTaskRequirements[7] = 1;
                currentTaskRequirements[8] = 0;
                break;

            case 5:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 1; //number = 5
                currentTaskRequirements[2] = 0;
                currentTaskRequirements[3] = 1;
                currentTaskRequirements[4] = 1; //size = large
                currentTaskRequirements[5] = 0;
                currentTaskRequirements[6] = 0; //color = blue
                currentTaskRequirements[7] = 0;
                currentTaskRequirements[8] = 1;
                break;

            case 6:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 1; //number = 7
                currentTaskRequirements[2] = 1;
                currentTaskRequirements[3] = 1;
                currentTaskRequirements[4] = 0; //size = large
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 1; //color = yellow
                currentTaskRequirements[7] = 1;
                currentTaskRequirements[8] = 0;
                break;

            case 7:
                currentTaskRequirements[0] = 1; //task is active
                currentTaskRequirements[1] = 0; //number = 6
                currentTaskRequirements[2] = 1;
                currentTaskRequirements[3] = 1;
                currentTaskRequirements[4] = 0; //size = x-large
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 0; //color = purple
                currentTaskRequirements[7] = 1;
                currentTaskRequirements[8] = 1;
                break;

            default:
                break;
        }

        taskStation.GetTaskButtonPushed(currentTask);
        numberStation.GetTaskButtonPushed(currentTask);
        sizeStation.GetTaskButtonPushed(currentTask);
        outputStation.GetTaskButtonPushed(currentTask);
    }

    public void RunOutputButtonPushed()
    {
        userProgression = UserProgression.runOutput;

        //taskStation.RunOutputButtonPushed();
        numberStation.RunOutputButtonPushed();
        sizeStation.RunOutputButtonPushed();
        outputStation.RunOutputButtonPushed(GetNumber(), currentColor, size);
    }

    public void PlacedOutput()
    {
        taskStation.PlacedOutput();
    }

    public void RemovedOutput()
    {
        taskStation.RemovedOutput();
    }

    public void OutputSent()
    {
        int number = GetNumber();
        outputStation.OutputSent(number);
        switch (currentTask)
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
        }
    }

    public void LevelComplete()
    {
        //Disable everything
        taskStation.LevelComplete();
    }

    public void SetNumber(int bit, int bitStatus)
    {
        //Debug.Log("SetNumber bit " + bit.ToString() + " bitStatus " + bitStatus.ToString());
        currentTaskStatus[bit] = bitStatus;
        numberStation.UpdateBitText(bit, bitStatus);
        outputStation.UpdateNumText(GetNumber());
    }

    public void SetSize(int bit, int bitStatus)
    {
        currentTaskStatus[bit] = bitStatus;
        if (currentTaskStatus[4] == 0)
        {
            if (currentTaskStatus[5] == 0) size = "Small";//small
            else size = "Large";//large
        }
        else
        {
            if (currentTaskStatus[5] == 1) size = "X-Large";//x-large
            else size = "Medium";//medium
        }
        sizeStation.UpdateSizeText(size);
        outputStation.UpdateSizeText(size);
    }

    public int GetNumber()
    {
        return (currentTaskStatus[1] + (2 * currentTaskStatus[2]) + (4 * currentTaskStatus[3]));
    }
}
