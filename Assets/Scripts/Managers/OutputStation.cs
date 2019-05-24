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
    public LeverPulled leverRunOutput;
    private LevelManager levelManager;
    public GameObject cube, sphere, cone, diamond;
    private GameObject objectSpawn;
    private Vector3 objectPosition;
    private int spawnedObjects;

    private void Start()
    {
        levelManager = transform.root.GetComponent<LevelManager>();
        if (Application.isEditor) transform.position = new Vector3(-2f, 0.2f, 0.0f); //Sitting Rift position
    }

    public void SetTask()
    {
        //runButton.ResetButton(false);
        leverRunOutput.SetTask();
        stationText.text = "No output currently available...";
        numberItem.text = "0";
        //sizeItem.text = "Small";
        colorItem.text = "Black";
        shapeItem.text = "Cubes";
        //ResetCubeGrabbable(); //only useful for user resetting level with A button
    }

    public void GetTaskLeverPulled(int currentTask)
    {
        //runButton.ResetButton(true);
        UpdateNumText(0);
        ResetCubeGrabbable();
    }

    public void RunOutputButtonPushed(int number, Color currentColor, string shape)
    {
        levelManager.RunOutput(); //to reset the sendOutputLever

        stationText.text = "Output generated\nPlease take output to task station";
        cubeGrabbable.GetComponent<Collider>().enabled = true;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = true;
        Debug.Log(number.ToString() + currentColor.ToString() + shape.ToString());
        for (int i = 0; i < number; i++)
        {
            if (shape == "Cube") objectSpawn = Instantiate(cube);
            if (shape == "Sphere") objectSpawn = Instantiate(sphere);
            if (shape == "Cone") objectSpawn = Instantiate(cone);
            if (shape == "Torus") objectSpawn = Instantiate(diamond);
            objectPosition = new Vector3(cubeGrabbable.position.x + Random.Range(-0.05f, 0.05f), cubeGrabbable.position.y + Random.Range(-0.2f, 0.2f), cubeGrabbable.position.z + Random.Range(-0.05f, 0.05f));
            objectSpawn.transform.position = objectPosition;
            objectSpawn.transform.parent = cubeGrabbable;
            objectSpawn.GetComponent<RandomMovement>().isSent = false;
            objectSpawn.GetComponent<Renderer>().material.color = currentColor;
        }
    }

    public void OutputSent(int number)
    {
        foreach(Transform child in cubeGrabbable)
        {
            child.GetComponent<RandomMovement>().isSent = true;
        }
        /*
        for (int i = 0; i < number; i++)
        {
            cubeGrabbable.GetChild(i).GetComponent<RandomMovement>().isSent = true;
            //Debug.Log(cubeGrabbable.GetChild(i).GetComponent<RandomMovement>().isSent.ToString());
        }
        */
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
            //runButton.ResetButton(true);
            leverRunOutput.GetTaskButtonPushed(1);
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
            //runButton.ResetButton(false);
            leverRunOutput.SetTask();
        }
    }

    public void UpdateShapeText(string shape)
    {
        shapeItem.text = shape;
    }

    public void UpdateColorText(Color color)
    {
        if (color.r == 1 && color.b == 1 && color.g == 1) colorItem.text = "White";
        if (color.r == 1 && color.b == 1 && color.g == 0) colorItem.text = "Purple";
        if (color.r == 1 && color.b == 0 && color.g == 0) colorItem.text = "Red";
        if (color.r == 1 && color.b == 0 && color.g == 1) colorItem.text = "Yellow";
        if (color.r == 0 && color.b == 1 && color.g == 1) colorItem.text = "Cyan";
        if (color.r == 0 && color.b == 0 && color.g == 1) colorItem.text = "Green";
        if (color.r == 0 && color.b == 1 && color.g == 0) colorItem.text = "Blue";
        if (color.r == 0 && color.b == 0 && color.g == 0) colorItem.text = "Black";
    }

    private void ResetCubeGrabbable()
    {
        levelManager.ForceGrabberRelease(cubeGrabbable.GetComponent<OVRGrabbable>());
        cubeGrabbable.GetComponent<Collider>().enabled = false;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = false;
        cubeGrabbable.position = cubeGrabbleStartPosition.position;
        cubeGrabbable.rotation = cubeGrabbleStartPosition.rotation;

        foreach (Transform child in cubeGrabbable)
        {
            Destroy(child.gameObject);
        }
        /*
        for (int i = 0; i < spawnedObjects; i++)
        {
            Destroy(cubeGrabbable.GetChild(i).gameObject);
        }*/
        /*
        spawnedObjects = cubeGrabbable.childCount;
        if(spawnedObjects > 0)
        {
            for (int i = 0; i < spawnedObjects; i++)
            {
                Destroy(cubeGrabbable.GetChild(i).gameObject);
            }
        }*/
        
    }
}
