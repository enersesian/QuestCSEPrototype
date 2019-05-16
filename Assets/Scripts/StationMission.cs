using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationMission : MonoBehaviour
{
    public Text taskText;

    public void StartLevel()
    {
        taskText.text = "Waiting for new task...";
        //getTaskButtonAnim.SetBool("isPushed", false);
    }

    public void GetTask()
    {
        //getTaskButtonAnim.SetBool("isPushed", true);
        taskText.text = "Please bring back one blue cube. \nStart at the number station";
    }
}
