using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationNumber : MonoBehaviour
{
    public GameObject leverTop;
    public Text stationText, bit01, bit02, bit03, bit04;
    private int totalCount;
    private StationManager stationManager;

    private void Start()
    {
        stationManager = transform.parent.GetComponent<StationManager>();
    }

    public void StartLevel()
    {
        leverTop.GetComponent<SphereCollider>().enabled = false;
        leverTop.GetComponent<Renderer>().material.SetColor("_Color", transform.parent.GetComponent<StationManager>().disabledColor);
        leverTop.GetComponent<StationNumberLeverControl>().StartLevel();
        stationText.text = "Waiting for new task...";
        bit01.text = "";
        bit02.text = "";
        bit03.text = "";
        bit04.text = "";
        //totalCount = 0;
    }

    public void GetTask(int currentTask)
    {
        switch(currentTask)
        {
            case 1:
                leverTop.GetComponent<SphereCollider>().enabled = true;
                leverTop.GetComponent<Renderer>().material.SetColor("_Color", transform.parent.GetComponent<StationManager>().activeColor);
                break;

            default:
                break;
        }

        stationText.text = "Total = 0";
        bit01.text = "0";
        bit02.text = "0";
        bit03.text = "0";
        bit04.text = "0";
    }

    public void UpdateBitText(int bit, int bitStatus)
    {
        //totalCount = 0;
        switch(bit)
        {
            case 1:
                if (bitStatus == 1)
                {
                    bit01.text = "1";
                    //totalCount++;
                }
                else bit01.text = "0";
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;

            default:
                break;
        }
        stationText.text = "Total = " + stationManager.GetNumber().ToString();
        //transform.parent.GetComponent<StationManager>().stationOutput.UpdateUI(totalCount);
    }
}
