using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberStation : MonoBehaviour
{
    public LeverPulled[] levers;
    public Text stationText, bit01, bit02, bit03, bit04;
    public Transform farLocation, nearLocation;
    private int totalCount;

    public void SetLevelDistance(bool isNear) //Sets distance of station based on 20'x20' or 12'x12' space
    {
        if (isNear) transform.position = farLocation.position;
        else transform.position = nearLocation.position;
    }

    public void SetTask()
    {
        foreach(LeverPulled lever in levers) lever.SetTask();
        stationText.text = "0";
        /*bit01.text = "0";
        bit02.text = "0";
        bit03.text = "0";
        bit04.text = "0";*/
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
        /*bit01.text = "0";
        bit02.text = "0";
        bit03.text = "0";
        bit04.text = "0";*/
    }

    public void RunOutputButtonPushed()
    {
        foreach (LeverPulled lever in levers) lever.Deactivate();
    }

    public void UpdateBitText(int bit, int bitStatus)
    {
        /*switch(bit)
        {
            case 1:
                if (bitStatus == 1) bit01.text = "1";
                else bit01.text = "0";
                break;

            case 2:
                if (bitStatus == 1) bit02.text = "2";
                else bit02.text = "0";
                break;

            case 3:
                if (bitStatus == 1) bit03.text = "4";
                else bit03.text = "0";
                break;

            case 4:
                if (bitStatus == 1) bit04.text = "8";
                else bit04.text = "0";
                break;

            default:
                break;
        }*/
        stationText.text = LevelManager.instance.GetNumber().ToString();
    }
}
