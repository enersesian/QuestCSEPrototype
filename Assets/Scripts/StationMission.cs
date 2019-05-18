using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationMission : MonoBehaviour
{
    public Text taskText;
    public ButtonPushable getButton;

    public void StartLevel()
    {
        taskText.text = "Waiting for new task...\nPush Get Task Button";
        getButton.ResetButton(true);
    }

    public void GetTask(int currentTask)
    {
        if(currentTask == 1)
        {
            taskText.text = "Welcome to task one \nPlease bring back one blue cube. \nStart at the number station";
        }
    }

    public void SendOutput()
    {
        taskText.text = "You performed the task perfectly! Congratulations!";
    }
}
