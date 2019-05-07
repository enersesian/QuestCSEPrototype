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

    // Use this for initialization
    void Start ()
    {
        StartLevel();
	}

    public void StartLevel()
    {
        userProgression = UserProgression.startLevel;

        stationMission.StartLevel();
        stationNumber.StartLevel();
        stationOutput.StartLevel();
    }

    public void GetTask()
    {
        userProgression = UserProgression.getTask;

        stationMission.GetTask();
        stationNumber.GetTask();
        stationOutput.GetTask();
    }
}
