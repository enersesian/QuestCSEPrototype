using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputStation : MonoBehaviour
{
    public Color activeColor;
    public Text stationText, numberItem, colorItem, shapeItem, sizeItem;
    public Transform cubeGrabbable, cubeGrabbleStartPosition, spawnLocation, tempSpawnLocation, farLocation, nearLocation;
    public ButtonPushed runButton;
    public LeverPulled leverRunOutput;
    public GameObject cube, sphere, cone, ring, currentShapeSelection;
    public GameObject[] objectIconsLarge;
    private GameObject objectSpawn;
    private Vector3 objectPosition;
    private int spawnedObjects;

    public void SetLevelDistance(bool isNear)
    {
        if (isNear) transform.position = farLocation.position;
        else transform.position = nearLocation.position;
    }

    public void SetTask()
    {
        //runButton.ResetButton(false);
        leverRunOutput.SetTask();
        stationText.text = "No output currently available...";
        numberItem.text = "-";
        //sizeItem.text = "Small";
        colorItem.text = "-";
        shapeItem.text = "-";
        currentShapeSelection = cube;
        //ResetCubeGrabbable(); //only useful for user resetting level with A button
        foreach (Transform child in spawnLocation)
        {
            Destroy(child.gameObject);
        }
    }

    public void GetTaskLeverPulled(int currentTask)
    {
        //runButton.ResetButton(true);
        //UpdateNumText(0);
        ResetCubeGrabbable();
    }

    public void RunOutputButtonPushed(int number, Color currentColor, string shape)
    {
        LevelManager.instance.RunOutput(); //to reset the sendOutputLever

        stationText.text = "Output generated\nPlease take output to task station";
        cubeGrabbable.GetComponent<Collider>().enabled = true;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = true;
        Debug.Log(number.ToString() + currentColor.ToString() + shape.ToString());

        if (shape == "Cube") currentShapeSelection = cube;
        if (shape == "Sphere") currentShapeSelection = sphere;
        if (shape == "Cone") currentShapeSelection = cone;
        if (shape == "Ring") currentShapeSelection = ring;
        for (int i = 0; i < number; i++)
        {
            objectSpawn = Instantiate(currentShapeSelection);
            objectPosition = new Vector3(cubeGrabbable.position.x + Random.Range(-0.05f, 0.05f), 
                cubeGrabbable.position.y + Random.Range(-0.2f, 0.2f), cubeGrabbable.position.z + Random.Range(-0.05f, 0.05f));
            objectSpawn.transform.position = objectPosition;
            objectSpawn.transform.parent = cubeGrabbable;
            objectSpawn.GetComponent<RandomMovement>().isSent = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = currentColor;
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
            stationText.text = "Hit button to generate output:";
            numberItem.text = LevelManager.instance.GetNumber().ToString();
            colorItem.text = LevelManager.instance.GetColorText();
            shapeItem.text = LevelManager.instance.GetShape();
            if (number > 1) shapeItem.text += "s";
            //runButton.ResetButton(true);
            leverRunOutput.GetTaskButtonPushed(1);
            //destroy current objects
            foreach (Transform child in spawnLocation)
            {
                Destroy(child.gameObject);
            }

            if (LevelManager.instance.GetShape() == "Cube") currentShapeSelection = cube;
            if (LevelManager.instance.GetShape() == "Sphere") currentShapeSelection = sphere;
            if (LevelManager.instance.GetShape() == "Cone") currentShapeSelection = cone;
            if (LevelManager.instance.GetShape() == "Ring") currentShapeSelection = ring;
            //build new objects
            if (number == 1)
            {
                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

            }
            if(number == 2)
            {
                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z - 0.1f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z + 0.1f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;
            }
            if(number == 3)
            {
                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z - 0.2f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z + 0.2f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;
            }
            if (number == 4)
            {
                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z - 0.1f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z + 0.1f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z - 0.3f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z + 0.3f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;
            }

            if (number == 5)
            {
                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z - 0.1f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z + 0.1f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                //second row

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z - 0.2f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z + 0.2f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;
            }
            if (number == 6)
            {
                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z - 0.2f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z + 0.2f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                //second row

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z - 0.2f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z + 0.2f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;
            }
            if (number == 7)
            {
                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z - 0.2f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z + 0.2f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                //second row

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z - 0.1f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z + 0.1f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z - 0.3f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.parent = spawnLocation;
                objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z + 0.3f);
                objectSpawn.transform.position = objectPosition;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.localScale *= 5f;

            }
        }
        else
        {
            stationText.text = "No output currently available...";
            numberItem.text = "-";
            colorItem.text = "-";
            shapeItem.text = "-";
            //runButton.ResetButton(false);
            leverRunOutput.SetTask();
            //destroy current objects
            foreach (Transform child in spawnLocation)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void UpdateShapeText(string shape)
    {
        shapeItem.text = shape;
        //read in each child's information, destroy it and create new shape with color and location
        Debug.Log("UpdateShapeText method");
        if (LevelManager.instance.GetNumber() > 0)
        {
            if (shape == "Cube") currentShapeSelection = cube;
            if (shape == "Sphere") currentShapeSelection = sphere;
            if (shape == "Cone") currentShapeSelection = cone;
            if (shape == "Ring") currentShapeSelection = ring;

            for (int i = 0; i < LevelManager.instance.GetNumber(); i++)
            {
                /*
                if (shape == "Cube") child.GetComponent<MeshFilter>().mesh = cube.GetComponent<MeshFilter>().mesh;
                if (shape == "Sphere") child.GetComponent<MeshFilter>().mesh = sphere.GetComponent<MeshFilter>().mesh;
                if (shape == "Cone") child.GetComponent<MeshFilter>().mesh = cone.GetComponent<MeshFilter>().mesh;
                if (shape == "Ring") child.GetComponent<MeshFilter>().mesh = ring.GetComponent<MeshFilter>().mesh;
                */
                Debug.Log("Went through spawnLocation "+ i.ToString());
                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.position = spawnLocation.GetChild(i).position;
                objectSpawn.transform.localScale = spawnLocation.GetChild(i).localScale;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.parent = tempSpawnLocation;
                objectSpawn.GetComponent<RandomMovement>().enabled = false;
            }

            for (int i = 0; i < LevelManager.instance.GetNumber(); i++)
            {
                Destroy(spawnLocation.GetChild(i).gameObject);
                Debug.Log("Delete child of spawnLocation " + i.ToString());
            }

            for (int i = 0; i < LevelManager.instance.GetNumber(); i++)
            {
                Debug.Log("Move child of tempSpawnLocation " + i.ToString());
                tempSpawnLocation.GetChild(0).parent = spawnLocation; //prevents infinite loop crash as I am adding elements to a list that I am deleting elements from as well
            }
        }
        
    }

    public void UpdateColorText(Color currentColor, string currentColorText)
    {
        colorItem.text = currentColorText;
        //change all child's color
        foreach (Transform child in spawnLocation)
        {
            child.GetChild(0).GetComponent<Renderer>().material.color = currentColor;
        }
    }

    private void ResetCubeGrabbable()
    {
        UserManager.instance.ForceGrabberRelease(cubeGrabbable.GetComponent<OVRGrabbable>());
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
