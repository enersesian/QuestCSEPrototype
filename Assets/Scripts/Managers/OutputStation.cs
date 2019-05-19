using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputStation : MonoBehaviour
{
    public Color activeColor;
    public Text stationText;
    public Transform cubeGrabbable, cubeGrabbleStartPosition;
    public ButtonPushed runButton;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = transform.root.GetComponent<LevelManager>();
    }

    public void SetTask()
    {
        runButton.ResetButton(false);
        stationText.text = "";
        //ResetCubeGrabbable();
    }

    public void GetTaskButtonPushed(int currentTask)
    {
        runButton.ResetButton(true);
        UpdateUI(0);
        ResetCubeGrabbable();
    }

    public void RunOutputButtonPushed()
    {
        stationText.text = "Output generated\nPlease take output to task station";
        cubeGrabbable.GetComponent<BoxCollider>().enabled = true;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = true;
        if (transform.parent.GetComponent<LevelManager>().GetNumber() == 1)
        {
            cubeGrabbable.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OutputSent()
    {
        cubeGrabbable.GetChild(0).GetComponent<RandomMovement>().isSent = true;
        //cubeGrabbable.GetChild(0).parent = this.transform;
    }

    public void UpdateUI(int number)
    {
        stationText.text = "Hit button to generate:\n" + number.ToString() + " blue cube(s)";
    }

    private void ResetCubeGrabbable()
    {
        levelManager.ForceGrabberRelease(cubeGrabbable.GetComponent<OVRGrabbable>());
        cubeGrabbable.GetComponent<BoxCollider>().enabled = false;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = false;
        cubeGrabbable.position = cubeGrabbleStartPosition.position;
        cubeGrabbable.rotation = cubeGrabbleStartPosition.rotation;

        cubeGrabbable.GetChild(0).GetComponent<RandomMovement>().isSent = false;
        cubeGrabbable.GetChild(0).gameObject.SetActive(false);
        cubeGrabbable.GetChild(0).localPosition = Vector3.zero;
        //cubeGrabbable.GetChild(0).parent = cubeGrabbable.transform;
    }
}
