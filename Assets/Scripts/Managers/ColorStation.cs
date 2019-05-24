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
        if (Application.isEditor) transform.position = new Vector3(0.4f, 0.35f, -0.4f); //Sitting Rift position
        colorText.text = "black";
    }

    public void SetTask()
    {
        foreach(LeverPulled lever in levers) lever.SetTask();
        UpdateColorText(LevelManager.GetColor());
    }

    public void UpdateColorText(Color currentColor)
    {
        finalColor.color = currentColor;
        if (finalColor.color.r == 1 && finalColor.color.b == 1 && finalColor.color.g == 1) colorText.text = "White";
        if (finalColor.color.r == 1 && finalColor.color.b == 1 && finalColor.color.g == 0) colorText.text = "Purple";
        if (finalColor.color.r == 1 && finalColor.color.b == 0 && finalColor.color.g == 0) colorText.text = "Red";
        if (finalColor.color.r == 1 && finalColor.color.b == 0 && finalColor.color.g == 1) colorText.text = "Yellow";
        if (finalColor.color.r == 0 && finalColor.color.b == 1 && finalColor.color.g == 1) colorText.text = "Cyan";
        if (finalColor.color.r == 0 && finalColor.color.b == 0 && finalColor.color.g == 1) colorText.text = "Green";
        if (finalColor.color.r == 0 && finalColor.color.b == 1 && finalColor.color.g == 0) colorText.text = "Blue";
        if (finalColor.color.r == 0 && finalColor.color.b == 0 && finalColor.color.g == 0) colorText.text = "Black";
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
