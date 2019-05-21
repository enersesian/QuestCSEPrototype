﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskStation : MonoBehaviour
{
    public Text taskText;
    public ButtonPushed getButton, sendOutputButton;

    public void SetTask(string condition)
    {
        if (condition == "start")
        {
            taskText.text = "Waiting for new task...\nPress the Get Task button.";
            sendOutputButton.ResetButton(false); //only reset at beginning as pushing this button will execute SetTask in game and can cause double execution
        }
        if(condition == "success") taskText.text = "You performed the task perfectly! \nPress Get Task button to receive next task.";
        if(condition == "failure") taskText.text = "You did not performed the task correctly. \nPress Get Task button to try the task again.";
        getButton.ResetButton(true);
    }

    public void LevelComplete()
    {
        taskText.text = "You win! Awesome Job! Now get out of here!";
    }

    public void GetTaskButtonPushed(int currentTask)
    {
        sendOutputButton.ResetButton(false);

        if (currentTask == 1)
        {
            taskText.text = "Welcome to task one \nPlease bring back one small red cube. \nUse the number station.";
        }

        if (currentTask == 2)
        {
            taskText.text = "Welcome to task two \nPlease bring back two small red cubes. \nUse the number station.";
        }

        if (currentTask == 3)
        {
            taskText.text = "Welcome to task three \nPlease bring back four medium red cubes. \nUse the number and size stations.";
        }

        if (currentTask == 4)
        {
            taskText.text = "Welcome to task four \nPlease bring back three medium green cubes. \nUse the number, size, and color stations.";
        }

        if (currentTask == 5)
        {
            taskText.text = "Welcome to task five \nPlease bring back five large blue cubes. \nUse the number, size, and color stations.";
        }

        if (currentTask == 6)
        {
            taskText.text = "Welcome to task six \nPlease bring back seven large yellow cubes. \nUse the number, size, and color stations.";
        }

        if (currentTask == 6)
        {
            taskText.text = "Welcome to task seven \nPlease bring back six x-large purple cubes. \nUse the number, size, and color stations.";
        }
    }

    public void PlacedOutput()
    {
        sendOutputButton.ResetButton(true);
    }

    public void RemovedOutput()
    {
        sendOutputButton.ResetButton(false);
    }
}
