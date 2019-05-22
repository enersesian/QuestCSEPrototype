using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeStation : MonoBehaviour
{
    public LeverPulled[] levers;
    public Text stationText;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = transform.root.GetComponent<LevelManager>();
    }

    public void SetTask()
    {
        foreach(LeverPulled lever in levers) lever.SetTask();
        stationText.text = "Small";
    }

    public void GetTaskButtonPushed(int currentTask)
    {
        switch(currentTask)
        {
            case 1:
                break;

            case 2:
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

    public void UpdateSizeText(string size)
    {
        stationText.text = size;
    }
}
