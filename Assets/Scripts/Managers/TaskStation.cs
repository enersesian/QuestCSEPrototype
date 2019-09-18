using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskStation : MonoBehaviour
{
    public Text taskText, taskNumberText;
    public LeverPulled leverGetTask, leverSendOutput;
    public Transform spawnLocation, miniSpawnLocation, farLocation, nearLocation;
    public Sprite cubeIcon, sphereIcon, coneIcon, ringIcon;
    public Image taskColorImage;
    public Transform taskShapeList;

    public void SetLevelDistance(bool isNear) //Sets distance of station based on 20'x20' or 12'x12' space
    {
        if (isNear) transform.position = farLocation.position;  //near to far
        else transform.position = nearLocation.position;
    }

    public void SetTask(string condition)
    {
        if (condition == "start") taskText.text = "Waiting for new task...\nPull lever on right to get task.";
        if(condition == "success") taskText.text = "You performed the task perfectly! \nPull lever on right for next task.";
        if(condition == "failure") taskText.text = "Incorrect Output! \nPull lever on right to try again.";

        leverGetTask.SetTask();
    }

    public void LevelComplete()
    {
        taskText.text = "You win! Awesome job! Now get out of here!";
    }

    public void GetTaskLeverPulled(int currentTask)
    {
        if (currentTask == 1)
        {
            taskText.text = "Welcome to Task 1.\nBring back 1 Red Sphere.";
            taskNumberText.text = "1";
            taskShapeList.GetChild(0).gameObject.SetActive(false);
            taskShapeList.GetChild(1).gameObject.SetActive(true);
            taskShapeList.GetChild(2).gameObject.SetActive(false);
            taskShapeList.GetChild(3).gameObject.SetActive(false);
            taskColorImage.color = new Color(1f, 0f, 0f, 1f);
        }

        if (currentTask == 2)
        {
            taskText.text = "Welcome to Task 2.\nBring back 2 Red Spheres.";
            taskNumberText.text = "2";
            taskShapeList.GetChild(0).gameObject.SetActive(false);
            taskShapeList.GetChild(1).gameObject.SetActive(true);
            taskShapeList.GetChild(2).gameObject.SetActive(false);
            taskShapeList.GetChild(3).gameObject.SetActive(false);
            taskColorImage.color = new Color(1f, 0f, 0f, 1f);
        }

        if (currentTask == 3)
        {
            taskText.text = "Welcome to Task 3.\nBring back 4 Green Cubes.";
            taskNumberText.text = "4";
            taskShapeList.GetChild(0).gameObject.SetActive(true);
            taskShapeList.GetChild(1).gameObject.SetActive(false);
            taskShapeList.GetChild(2).gameObject.SetActive(false);
            taskShapeList.GetChild(3).gameObject.SetActive(false);
            taskColorImage.color = new Color(0f, 1f, 0f, 1f);
        }

        if (currentTask == 4)
        {
            taskText.text = "Welcome to Task 4.\nBring back 3 Blue Cones.";
            taskNumberText.text = "3";
            taskShapeList.GetChild(0).gameObject.SetActive(false);
            taskShapeList.GetChild(1).gameObject.SetActive(false);
            taskShapeList.GetChild(2).gameObject.SetActive(true);
            taskShapeList.GetChild(3).gameObject.SetActive(false);
            taskColorImage.color = new Color(0f, 0f, 1f, 1f);
        }

        if (currentTask == 5)
        {
            taskText.text = "Welcome to Task 5.\nBring back 5 Yellow Rings.";
            taskNumberText.text = "5";
            taskShapeList.GetChild(0).gameObject.SetActive(false);
            taskShapeList.GetChild(1).gameObject.SetActive(false);
            taskShapeList.GetChild(2).gameObject.SetActive(false);
            taskShapeList.GetChild(3).gameObject.SetActive(true);
            taskColorImage.color = new Color(1f, 1f, 0f, 1f);
        }

        if (currentTask == 6)
        {
            taskText.text = "Welcome to Task 6.\nBring back 7 Cyan Cones.";
            taskNumberText.text = "6";
            taskShapeList.GetChild(0).gameObject.SetActive(false);
            taskShapeList.GetChild(1).gameObject.SetActive(false);
            taskShapeList.GetChild(2).gameObject.SetActive(true);
            taskShapeList.GetChild(3).gameObject.SetActive(false);
            taskColorImage.color = new Color(0f, 1f, 1f, 1f);
        }

        if (currentTask == 7)
        {
            taskText.text = "Welcome to Task 7.\nBring back 6 Purple Ring.";
            taskNumberText.text = "7";
            taskShapeList.GetChild(0).gameObject.SetActive(false);
            taskShapeList.GetChild(1).gameObject.SetActive(false);
            taskShapeList.GetChild(2).gameObject.SetActive(false);
            taskShapeList.GetChild(3).gameObject.SetActive(true);
            taskColorImage.color = new Color(1f, 0f, 1f, 1f);
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
