using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeStation : MonoBehaviour
{
    public LeverPulled[] levers;
    public Text stationText, bit01, bit02, bit03, bit04;
    private int totalCount;
    private LevelManager LevelManager;

    private void Start()
    {
        LevelManager = transform.root.GetComponent<LevelManager>();
    }

    public void SetTask()
    {
        foreach(LeverPulled lever in levers) lever.SetTask();
        stationText.text = "Small";
        /*bit01.text = "0";
        bit02.text = "0";
        bit03.text = "0";
        bit04.text = "0";*/
    }

    public void GetTaskButtonPushed(int currentTask)
    {
        switch(currentTask)
        {
            case 1:
                break;

            case 2:
                break;

            default:
                levers[0].GetTaskButtonPushed(currentTask);
                levers[1].GetTaskButtonPushed(currentTask);
                break;
        }
        //stationText.text = "0";
        /*bit01.text = "0";
        bit02.text = "0";
        bit03.text = "0";
        bit04.text = "0";*/
    }

    public void RunOutputButtonPushed()
    {
        foreach (LeverPulled lever in levers) lever.RunOutputButtonPushed();
    }

    public void UpdateSizeText(string size)
    {
        stationText.text = size;
    }
}
