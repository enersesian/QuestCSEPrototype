﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberStation : MonoBehaviour
{
    public LeverPulled[] levers;
    public Text stationText, bit01, bit02, bit03, bit04;
    public Transform farLocation, nearLocation;
    private int totalCount;
    private LevelManager levelManager;


    private void Start()
    {
        levelManager = transform.root.GetComponent<LevelManager>();
        //if (Application.isEditor) transform.position = new Vector3(0.5f, 0.2f, 0.2f); //Sitting Rift position
    }

    public void SetLevelDistance(bool isNear)
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
                levers[0].GetTaskButtonPushed(currentTask);
                break;

            case 2:
                levers[0].GetTaskButtonPushed(currentTask);
                levers[1].GetTaskButtonPushed(currentTask);
                break;

            default:
                levers[0].GetTaskButtonPushed(currentTask);
                levers[1].GetTaskButtonPushed(currentTask);
                levers[2].GetTaskButtonPushed(currentTask);
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
        foreach (LeverPulled lever in levers) lever.RunOutputButtonPushed();
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
        stationText.text = levelManager.GetNumber().ToString();
    }
}