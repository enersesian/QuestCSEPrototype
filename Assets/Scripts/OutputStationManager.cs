using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputStationManager : MonoBehaviour
{
    public GameObject runButtonCollider, runButton;
    public Color activeColor;
    public Text stationText;
    public stationNumber stationNumberMng;

    public void ActivateStation()
    {
        runButtonCollider.GetComponent<BoxCollider>().enabled = true;
        stationText.text = "Hit button to generate:\n" + stationNumberMng.GetTotalCount() + " blue cube(s)";
        //runButton.GetComponent<Renderer>().material.SetColor("_Color", activeColor);
    }

}
