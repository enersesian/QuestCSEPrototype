using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputStation : MonoBehaviour
{
    public Color activeColor;
    public Text stationText, numberItem, colorItem, shapeItem, sizeItem;
    public Transform cubeGrabbable, cubeGrabbleStartPosition;
    public ButtonPushed runButton;
    private LevelManager levelManager;
    public GameObject cube;
    private GameObject cubeSpawn;

    private void Start()
    {
        levelManager = transform.root.GetComponent<LevelManager>();
    }

    public void SetTask()
    {
        runButton.ResetButton(false);
        stationText.text = "No output currently available...";
        numberItem.text = "-";
        sizeItem.text = "-";
        colorItem.text = "-";
        shapeItem.text = "-";
        //ResetCubeGrabbable();
    }

    public void GetTaskButtonPushed(int currentTask)
    {
        runButton.ResetButton(true);
        UpdateUI(0);
        ResetCubeGrabbable();
    }

    public void RunOutputButtonPushed(int number)
    {
        stationText.text = "Output generated\nPlease take output to task station";
        cubeGrabbable.GetComponent<BoxCollider>().enabled = true;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = true;

        Vector3 cubePosition;
        for (int i = 0; i < number; i++)
        {
            cubePosition = new Vector3(cubeGrabbable.position.x + Random.Range(-0.05f, 0.05f), cubeGrabbable.position.y + Random.Range(-0.2f, 0.2f), cubeGrabbable.position.z + Random.Range(-0.05f, 0.05f));
            cubeSpawn = Instantiate(cube, cubePosition, Quaternion.identity);
            cubeSpawn.transform.parent = cubeGrabbable;
            cubeSpawn.GetComponent<RandomMovement>().isSent = false;
        }
    }

    public void OutputSent(int number)
    {
        Debug.Log(number.ToString());
        //Vector3 cubePosition = new Vector3(cubeGrabbable.position.x + Random.Range(-0.1f, 0.1f), cubeGrabbable.position.y + Random.Range(-0.1f, 0.1f), cubeGrabbable.position.z + Random.Range(-0.1f, 0.1f));
        for (int i = 0; i < number; i++)
        {
            cubeGrabbable.GetChild(i).GetComponent<RandomMovement>().isSent = true;
            Debug.Log(cubeGrabbable.GetChild(i).GetComponent<RandomMovement>().isSent.ToString());
        }
    }

    public void UpdateUI(int number)
    {
        if (number > 0)
        {
            stationText.text = "Hit button to generate:\n" + number.ToString() + " small white cube(s)";
            numberItem.text = number.ToString();
            sizeItem.text = "Small";
            colorItem.text = "Blue";
            shapeItem.text = "Cube";
            runButton.ResetButton(true);
        }
        else
        {
            stationText.text = "No output currently available...";
            numberItem.text = "-";
            runButton.ResetButton(false);
        }
    }

    private void ResetCubeGrabbable()
    {
        levelManager.ForceGrabberRelease(cubeGrabbable.GetComponent<OVRGrabbable>());
        cubeGrabbable.GetComponent<BoxCollider>().enabled = false;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = false;
        cubeGrabbable.position = cubeGrabbleStartPosition.position;
        cubeGrabbable.rotation = cubeGrabbleStartPosition.rotation;

        for (int i = 0; i < cubeGrabbable.childCount; i++)
        {
            Destroy(cubeGrabbable.GetChild(i).gameObject);
        }
    }
}
