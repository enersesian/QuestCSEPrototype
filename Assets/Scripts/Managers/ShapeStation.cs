using System.Collections;
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
        //if (Application.isEditor) transform.position = new Vector3(-0.2f, 0.35f, -0.6f); //Sitting Rift position
    }

    public void SetLevelDistance(bool isNear) //Sets distance of station based on 20'x20' or 12'x12' space
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

    public void RunOutputButtonPushed()
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
