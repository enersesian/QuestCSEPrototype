using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskStation : MonoBehaviour
{
    public Text taskText;
    public ButtonPushed getButton, sendOutputButton;
    public LeverPulled leverGetTask, leverSendOutput;

    private void Start()
    {
        if (Application.isEditor) transform.position = new Vector3(0f, 0.2f, 0.6f); //Sitting Rift position
    }

    public void SetTask(string condition)
    {
        if (condition == "start")
        {
            taskText.text = "Waiting for new task...\nPress the Get Task button.";
            sendOutputButton.ResetButton(false); //only reset at beginning as pushing this button will execute SetTask in game and can cause double execution
        }
        if(condition == "success") taskText.text = "You performed the task perfectly! \nPress Get Task button to receive next task.";
        if(condition == "failure") taskText.text = "You did not performed the task correctly. \nPress Get Task button to try the task again.";
        //getButton.ResetButton(true);
        leverGetTask.SetTask();
        //leverSendOutput.SetTask();
    }

    public void LevelComplete()
    {
        taskText.text = "You win! Awesome Job! Now get out of here!";
    }

    public void GetTaskLeverPulled(int currentTask)
    {
        //sendOutputButton.ResetButton(false);
        //leverSendOutput.SetTask();

        if (currentTask == 1)
        {
            taskText.text = "Welcome to task one \nPlease bring back one red sphere. \nUse the number station.";
        }

        if (currentTask == 2)
        {
            taskText.text = "Welcome to task two \nPlease bring back two red spheres. \nUse the number station.";
        }

        if (currentTask == 3)
        {
            taskText.text = "Welcome to task three \nPlease bring back four green cubes. \nUse the number and size stations.";
        }

        if (currentTask == 4)
        {
            taskText.text = "Welcome to task four \nPlease bring back three blue cones. \nUse the number, size, and color stations.";
        }

        if (currentTask == 5)
        {
            taskText.text = "Welcome to task five \nPlease bring back five yellow diamonds. \nUse the number, size, and color stations.";
        }

        if (currentTask == 6)
        {
            taskText.text = "Welcome to task six \nPlease bring back seven cyan cones. \nUse the number, size, and color stations.";
        }

        if (currentTask == 7)
        {
            taskText.text = "Welcome to task seven \nPlease bring back six purple diamonds. \nUse the number, size, and color stations.";
        }
    }

    public void RunOutput()
    {
        //sendOutputButton.ResetButton(true);
        leverSendOutput.SetToZeroPosition();
    }

    public void PlacedOutput()
    {
        //sendOutputButton.ResetButton(true);
        leverSendOutput.Activate();
    }

    public void RemovedOutput()
    {
        //sendOutputButton.ResetButton(false);
        leverSendOutput.Deactivate();
    }
}
