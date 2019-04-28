using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stationNumber : MonoBehaviour {

    public GameObject levelTop;
    public Color activeColor;
    public Text stationText, bit01, bit02, bit03, bit04;
    private int totalCount = 0;
    public OutputStationManager outputMng;

    public void ActivateStation()
    {
        levelTop.GetComponent<SphereCollider>().enabled = true;
        levelTop.GetComponent<Renderer>().material.SetColor("_Color", activeColor);
        stationText.text = "Total = 0";
        bit01.text = "0";
        bit02.text = "0";
        bit03.text = "0";
        bit04.text = "0";
    }

    public void UpdateBitText(int bitNumber, int bitAmount)
    {
        totalCount = 0;
        switch(bitNumber)
        {
            case 1:
                if (bitAmount == 1)
                {
                    bit01.text = "1";
                    totalCount++;
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
        stationText.text = "Total = " + totalCount.ToString();
        outputMng.ActivateStation();

    }

    public int GetTotalCount()
    {
        return totalCount;
    }
}
