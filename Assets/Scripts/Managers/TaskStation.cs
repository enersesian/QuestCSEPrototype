using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskStation : MonoBehaviour
{
    public Text taskText;
    public ButtonPushed getButton;

    public void SetTask(string condition)
    {
        if(condition == "start") taskText.text = "Waiting for new task...\nPress the Get Task button.";
        if(condition == "success") taskText.text = "You performed the task perfectly! Congratulations! \nPress the Get Task button to receive next task.";
        if(condition == "failure") taskText.text = "You did not performed the task correctly. \nPress the Get Task button to try the task again.";
        getButton.ResetButton(true);
    }

    public void GetTaskButtonPushed(int currentTask)
    {
        if(currentTask == 1)
        {
            taskText.text = "Welcome to task one \nPlease bring back one blue cube. \nStart at the number station.";
        }

        if (currentTask == 2)
        {
            taskText.text = "Welcome to task two \nPlease bring back two blue cubes. \nStart at the number station.";
        }
    }
}
