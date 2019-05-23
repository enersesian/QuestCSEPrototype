using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorStation : MonoBehaviour
{
    public LeverPulled[] levers;
    public Image finalColor; 
    private int totalCount;
    private LevelManager LevelManager;

    private void Start()
    {
        LevelManager = transform.root.GetComponent<LevelManager>();
    }

    public void SetTask()
    {
        foreach(LeverPulled lever in levers) lever.SetTask();
        finalColor.color = new Color(1f, 0.33f, 0.33f);
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
    }

    public void RunOutputButtonPushed()
    {
        foreach (LeverPulled lever in levers) lever.RunOutputButtonPushed();
    }

}
