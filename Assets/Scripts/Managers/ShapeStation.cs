using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeStation : MonoBehaviour
{
    public LeverPulled[] levers;
    public Text stationText;

    private void Start()
    {
        //if (Application.isEditor) transform.position = new Vector3(-0.2f, 0.35f, -0.6f); //Sitting Rift position
    }

    public void SetTask()
    {
        foreach(LeverPulled lever in levers) lever.SetTask();
        stationText.text = "Cube";
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
                break;

            default:
                levers[0].GetTaskButtonPushed(currentTask);
                levers[1].GetTaskButtonPushed(currentTask);
                break;
        }
    }

    public void RunOutputButtonPushed()
    {
        foreach (LeverPulled lever in levers) lever.RunOutputButtonPushed();
    }

    public void UpdateShapeText(string shape)
    {
        stationText.text = shape;
    }
}
