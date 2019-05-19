using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public TaskStation taskStation;
    public NumberStation numberStation;
    public OutputStation outputStation;

    public Color activeColor, disabledColor, hitColor;
    public OVRGrabber leftHandGrabber, rightHandGrabber;

    private enum UserProgression { SetTask, getTask, runOutput };
    private UserProgression userProgression;

    private int currentTask;
    //0 = task, 1 = bit 1, 2 = bit 2, 3 = bit 3, 4 = bit 4, 5 = shape, 6 = color, 7 = size, 8 = tbd
    private int[] currentTaskStatus = new int[9]; 
    private int[] currentTaskRequirements = new int[9]; 

    void Start ()
    {
        SetLevel();
        SetTask("start", 1);
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

        taskStation.SetTask(condition);
        numberStation.SetTask();
        outputStation.SetTask();
    }

    public void ForceGrabberRelease(OVRGrabbable grabbable)
    {
        leftHandGrabber.ForceRelease(grabbable);
        rightHandGrabber.ForceRelease(grabbable);
    }

    public void GetTaskButtonPushed()
    {
        userProgression = UserProgression.getTask;
        switch(currentTask)
        {
            case 1:
                currentTaskRequirements[0] = 1;
                currentTaskRequirements[1] = 1;
                currentTaskRequirements[2] = 0;
                currentTaskRequirements[3] = 0;
                currentTaskRequirements[4] = 0;
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 1;
                currentTaskRequirements[7] = 1;
                currentTaskRequirements[8] = 1;
                break;

            case 2:
                currentTaskRequirements[0] = 1;
                currentTaskRequirements[1] = 0;
                currentTaskRequirements[2] = 1;
                currentTaskRequirements[3] = 0;
                currentTaskRequirements[4] = 0;
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 1;
                currentTaskRequirements[7] = 1;
                currentTaskRequirements[8] = 1;
                break;

            case 3:
                currentTaskRequirements[0] = 1;
                currentTaskRequirements[1] = 0;
                currentTaskRequirements[2] = 0;
                currentTaskRequirements[3] = 1;
                currentTaskRequirements[4] = 0;
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 1;
                currentTaskRequirements[7] = 1;
                currentTaskRequirements[8] = 1;
                break;

            case 4:
                currentTaskRequirements[0] = 1;
                currentTaskRequirements[1] = 0;
                currentTaskRequirements[2] = 0;
                currentTaskRequirements[3] = 0;
                currentTaskRequirements[4] = 1;
                currentTaskRequirements[5] = 1;
                currentTaskRequirements[6] = 1;
                currentTaskRequirements[7] = 1;
                currentTaskRequirements[8] = 1;
                break;

            default:
                break;
        }

        taskStation.GetTaskButtonPushed(currentTask);
        numberStation.GetTaskButtonPushed(currentTask);
        outputStation.GetTaskButtonPushed(currentTask);
    }

    public void RunOutputButtonPushed()
    {
        userProgression = UserProgression.runOutput;

        //taskStation.RunOutputButtonPushed();
        numberStation.RunOutputButtonPushed();
        outputStation.RunOutputButtonPushed(GetNumber());
    }

    public void OutputSent()
    {
        int number = GetNumber();
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

        outputStation.OutputSent(GetNumber());
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
        outputStation.UpdateUI(GetNumber());
    }

    public int GetNumber()
    {
        return (currentTaskStatus[1] + (2 * currentTaskStatus[2]) + (4 * currentTaskStatus[3]) + (8 * currentTaskStatus[4]));
    }
}
