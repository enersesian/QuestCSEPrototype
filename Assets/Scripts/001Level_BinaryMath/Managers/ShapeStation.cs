﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeStation : MonoBehaviour
{
    public LeverPulled[] levers;
    public Text stationText;
    public Transform farLocation, nearLocation;
    public Sprite[] shapes;
    public Image shapeImage;

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
        stationText.text = "Cube";
        shapeImage.sprite = shapes[0];
    }

    public void ActivateLever(int leverNumber)
    {
        levers[leverNumber].Activate();
    }

    public void GetTaskLeverPulled(int currentTask)
    {
        switch(currentTask)
        {
            case 1:
                break;

            case 2:
                levers[0].Activate();
                break;

            default:
                levers[0].Activate();
                levers[1].Activate();
                break;
        }
    }

    public void RunOutputLeverPulled()
    {
        foreach (LeverPulled lever in levers) lever.Deactivate();
    }

    public void UpdateShapeText(string shape)
    {
        stationText.text = shape;
        if(shape == "Cube") shapeImage.sprite = shapes[0];
        else if (shape == "Sphere") shapeImage.sprite = shapes[1];
        else if (shape == "Cone") shapeImage.sprite = shapes[2];
        else shapeImage.sprite = shapes[3];
    }
}
