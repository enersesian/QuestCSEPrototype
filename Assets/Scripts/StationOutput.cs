using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationOutput : MonoBehaviour
{
    public Color activeColor;
    public Text stationText;
    public Transform cubeGrabbable, cubeGrabbleStartPosition;
    public ButtonPushable runButton;

    public void StartLevel()
    {
        runButton.ResetButton(false);
        stationText.text = "";

        cubeGrabbable.position = cubeGrabbleStartPosition.position;
        cubeGrabbable.rotation = cubeGrabbleStartPosition.rotation;
        cubeGrabbable.GetComponent<BoxCollider>().enabled = false;

        cubeGrabbable.GetChild(0).GetComponent<RandomMovement>().isSent = false;
        cubeGrabbable.GetChild(0).gameObject.SetActive(false);
        cubeGrabbable.GetChild(0).localPosition = Vector3.zero;
    }

    public void GetTask()
    {
        runButton.ResetButton(true);
        UpdateUI(0);
    }

    public void UpdateUI(int number)
    {
        stationText.text = "Hit button to generate:\n" + number.ToString() + " blue cube(s)";
    }

    public void RunOutput()
    {
        stationText.text = "Output generated\nPlease take output to task station";
        cubeGrabbable.GetComponent<BoxCollider>().enabled = true;
        if (transform.parent.GetComponent<StationManager>().GetNumber() == 1)
        {
            cubeGrabbable.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void SendOutput()
    {
        cubeGrabbable.GetChild(0).GetComponent<RandomMovement>().isSent = true;
    }
}
