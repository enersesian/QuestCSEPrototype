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
        taskText.text = "Waiting for new task...";
        getButton.ResetButton(true);
    }

    public void GetTask()
    {
        taskText.text = "Please bring back one blue cube. \nStart at the number station";
    }

    public void SendOutput()
    {
        taskText.text = "You performed the task perfectly! Congratulations!";
    }
}
