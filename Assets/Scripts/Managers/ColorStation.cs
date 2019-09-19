using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorStation : MonoBehaviour
{
    public LeverPulled[] levers;
    public Image finalColor;
    public Text colorText;
    public Transform farLocation, nearLocation;
    private int totalCount;

    private void Start()
    {
        if (Application.isEditor) transform.position = nearLocation.position;
        colorText.text = "Black";
    }

    public void SetLevelDistance(bool isNear) //Sets distance of station based on 20'x20' or 12'x12' space
    {
        if (isNear) transform.position = farLocation.position;
        else transform.position = nearLocation.position;
    }

    public void SetTask()
    {
        foreach(LeverPulled lever in levers) lever.SetTask(); //deactivate and set to zero
        UpdateColorText(LevelManager.instance.GetColor(), LevelManager.instance.GetColorText());
    }

    public void UpdateColorText(Color currentColor, string currentColorText)
    {
        finalColor.color = currentColor;
        colorText.text = currentColorText;
    }

    public void ActivateLever(int leverNumber)
    {
        levers[leverNumber].Activate();
    }

    public void GetTaskLeverPulled(int currentTask)
    {
        switch(currentTask)
        {
            case 1:
                break;

            case 2:
                levers[0].Activate();
                break;

            case 3:
                levers[0].Activate();
                levers[1].Activate();
                break;

            default:
                levers[0].Activate();
                levers[1].Activate();
                levers[2].Activate();
                break;
        }
    }

    public void RunOutputButtonPushed()
    {
        foreach (LeverPulled lever in levers) lever.Deactivate();
    }

}
