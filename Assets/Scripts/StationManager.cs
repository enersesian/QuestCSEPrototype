using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationManager : MonoBehaviour
{
    public StationMission stationMission;
    public StationNumber stationNumber;
    public StationOutput stationOutput;

    public Color activeColor, disabledColor, hitColor;

    private enum UserProgression { startLevel, getTask, runOutput };
    private UserProgression userProgression;

    private int currentTask;
    //0 = task, 1 = bit 1, 2 = bit 2, 3 = bit 3, 4 = bit 4, 5 = shape, 6 = color, 7 = size, 8 = tbd
    private int[] currentTaskStatus = new int[9]; 
    private int[] currentTaskRequirements = new int[9]; 

    void Start ()
    {
        StartLevel();
	}

    public void StartLevel()
    {
        userProgression = UserProgression.startLevel;
        currentTask = 0;
        for(int i = 0; i < currentTaskStatus.Length; i++)
        {
            currentTaskStatus[i] = 0;
            currentTaskRequirements[i] = 0;
        }

        stationMission.StartLevel();
        stationNumber.StartLevel();
        stationOutput.StartLevel();
    }

    public void GetTask()
    {
        userProgression = UserProgression.getTask;
        currentTask++;
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

            default:
                break;
        }

        stationMission.GetTask(currentTask);
        stationNumber.GetTask(currentTask);
        stationOutput.GetTask();
    }

    public void SetNumber(int bit, int bitStatus)
    {
        currentTaskStatus[bit] = bitStatus;
        stationNumber.UpdateBitText(bit, bitStatus);
        stationOutput.UpdateUI(GetNumber());
    }

    public int GetNumber()
    {
        return currentTaskStatus[1] + 2 * currentTaskStatus[2] + 4 * currentTaskStatus[2] + 8 * currentTaskStatus[3];
    }

    public void RunOutput()
    {
        userProgression = UserProgression.runOutput;

        stationOutput.RunOutput();
    }

    public void SendOutput()
    {
        stationMission.SendOutput();
        stationOutput.SendOutput();
    }
}
