using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationOutput : MonoBehaviour
{
    public GameObject runButtonCollider, runButton;
    public Color activeColor;
    public Text stationText;
    public StationNumber stationNumberMng;
    public Transform cubeGrabbable, cubeGrabbleStartPosition;

    public void StartLevel()
    {
        runButtonCollider.GetComponent<BoxCollider>().enabled = false;
        runButton.GetComponent<Animator>().SetBool("isPushed", false);
        stationText.text = "";
        cubeGrabbable.position = cubeGrabbleStartPosition.position;
        cubeGrabbable.rotation = cubeGrabbleStartPosition.rotation;

        cubeGrabbable.GetChild(0).GetComponent<RandomMovement>().isSent = false;
        cubeGrabbable.GetChild(0).gameObject.SetActive(false);
        cubeGrabbable.GetChild(0).localPosition = Vector3.zero;
    }

    public void GetTask()
    {
        runButtonCollider.GetComponent<BoxCollider>().enabled = true;
        UpdateUI(0);
        //runButton.GetComponent<Renderer>().material.SetColor("_Color", activeColor);
    }

    public void UpdateUI(int totalCount)
    {
        stationText.text = "Hit button to generate:\n" + totalCount.ToString() + " blue cube(s)";
    }
}
