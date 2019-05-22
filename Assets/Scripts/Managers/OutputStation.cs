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
        if (Application.isEditor) transform.position = new Vector3(0f, 0.2f, 2.2f); //Sitting Rift position
    }

    public void SetTask()
    {
        runButton.ResetButton(false);
        stationText.text = "No output currently available...";
        numberItem.text = "0";
        sizeItem.text = "Small";
        colorItem.text = "Red";
        shapeItem.text = "Cubes";
        ResetCubeGrabbable(); //onlu useful for user resetting level with A button
    }

    public void GetTaskButtonPushed(int currentTask)
    {
        //runButton.ResetButton(true);
        UpdateNumText(0);
        ResetCubeGrabbable();
    }

    public void RunOutputButtonPushed(int number, Color currentColor, string size)
    {
        stationText.text = "Output generated\nPlease take output to task station";
        cubeGrabbable.GetComponent<Collider>().enabled = true;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = true;

        Vector3 cubePosition;
        for (int i = 0; i < number; i++)
        {
            cubePosition = new Vector3(cubeGrabbable.position.x + Random.Range(-0.05f, 0.05f), cubeGrabbable.position.y + Random.Range(-0.2f, 0.2f), cubeGrabbable.position.z + Random.Range(-0.05f, 0.05f));
            cubeSpawn = Instantiate(cube, cubePosition, Quaternion.identity);
            cubeSpawn.transform.parent = cubeGrabbable;
            cubeSpawn.GetComponent<RandomMovement>().isSent = false;
            cubeSpawn.GetComponent<Renderer>().material.color = currentColor;
            if (size == "small") cubeSpawn.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            if (size == "medium") cubeSpawn.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            if (size == "large") cubeSpawn.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            else cubeSpawn.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
    }

    public void OutputSent(int number)
    {
        for (int i = 0; i < number; i++)
        {
            cubeGrabbable.GetChild(i).GetComponent<RandomMovement>().isSent = true;
            //Debug.Log(cubeGrabbable.GetChild(i).GetComponent<RandomMovement>().isSent.ToString());
        }
    }

    public void UpdateNumText(int number)
    {
        if (number > 0)
        {
            stationText.text = "Hit button to generate output";
            numberItem.text = number.ToString();
            //if (sizeItem.text == "-") sizeItem.text = "Small";
            //if (colorItem.text == "-") colorItem.text = "Red";
            if (number > 1) shapeItem.text = "Cubes";
            else shapeItem.text = "Cube";
            runButton.ResetButton(true);
        }
        else
        {
            stationText.text = "No output currently available...";
            numberItem.text = number.ToString();
            shapeItem.text = "Cubes";
            /*numberItem.text = "-";
            sizeItem.text = "-";
            colorItem.text = "-";
            shapeItem.text = "-";*/
            runButton.ResetButton(false);
        }
    }

    public void UpdateSizeText(string size)
    {
        sizeItem.text = size;
    }

    private void ResetCubeGrabbable()
    {
        levelManager.ForceGrabberRelease(cubeGrabbable.GetComponent<OVRGrabbable>());
        cubeGrabbable.GetComponent<Collider>().enabled = false;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = false;
        cubeGrabbable.position = cubeGrabbleStartPosition.position;
        cubeGrabbable.rotation = cubeGrabbleStartPosition.rotation;

        for (int i = 0; i < cubeGrabbable.childCount; i++)
        {
            Destroy(cubeGrabbable.GetChild(i).gameObject);
        }
    }
}
