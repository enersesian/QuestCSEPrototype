using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberStation : MonoBehaviour
{
    public LeverPulled[] levers;
    public Text stationText;
    public Transform farLocation, nearLocation;
    private int totalCount;

    private void Start()
    {
        //if (Application.isEditor) transform.position = nearLocation.position;
    }

    //Currently not using, meant for Rift testing, but now use quick move feature
    //Sets distance of tutorial walls based on 20'x20' or 12'x12' space
    public void SetLevelDistance(bool isNear) 
    {
        if (isNear) transform.position = farLocation.position;
        else transform.position = nearLocation.position;
    }

    public void SetTask()
    {
        foreach(LeverPulled lever in levers) lever.SetTask();
        stationText.text = "0";
    }

    public void GetTaskLeverPulled(int currentTask)
    {
        switch(currentTask)
        {
            case 1:
                levers[0].Activate();
                break;

            case 2:
                levers[0].Activate();
                levers[1].Activate();
                break;

            default:
                levers[0].Activate();
                levers[1].Activate();
                levers[2].Activate();
                break;
        }
        stationText.text = "0";
    }

    public void RunOutputLeverPulled()
    {
        foreach (LeverPulled lever in levers) lever.Deactivate();
    }

    public void UpdateBitText(int number)
    {
        stationText.text = number.ToString();
    }
}
