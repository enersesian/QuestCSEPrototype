using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorStation : MonoBehaviour
{
    public LeverPulled[] levers;
    public Image finalColor;
    public Text colorText;
    private int totalCount;
    private LevelManager LevelManager;

    private void Start()
    {
        LevelManager = transform.root.GetComponent<LevelManager>();
        if (Application.isEditor) transform.position = new Vector3(0.4f, 0.2f, -0.4f); //Sitting Rift position
        colorText.text = "black";
    }

    public void SetTask()
    {
        foreach(LeverPulled lever in levers) lever.SetTask();
        UpdateColorText(LevelManager.GetColor(), LevelManager.GetColorText());
    }

    public void UpdateColorText(Color currentColor, string currentColorText)
    {
        finalColor.color = currentColor;
        colorText.text = currentColorText;
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

            case 3:
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
