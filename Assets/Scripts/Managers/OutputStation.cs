using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputStation : MonoBehaviour
{
    public Text stationText;
    public Transform cubeGrabbable, cubeGrabbleStartPosition, spawnLocation, tempSpawnLocation, farLocation, nearLocation;
    public ButtonPushed runButton;
    public LeverPulled leverRunOutput;
    public GameObject cube, sphere, cone, ring, currentShapeSelection;
    public GameObject[] objectIconsLarge;
    private GameObject objectSpawn;
    private Vector3 objectPosition;
    private int spawnedObjects;
    public Text taskNumberText;
    public Transform taskShapeList;
    public Image taskColorImage;

    private void Start()
    {
        //if (Application.isEditor) transform.position = nearLocation.position;
    }


    public void SetLevelDistance(bool isNear) //Sets distance of station based on 20'x20' or 12'x12' space
    {
        if (isNear) transform.position = farLocation.position;
        else transform.position = nearLocation.position;
    }

    public void SetTask()
    {
        leverRunOutput.SetTask();
        stationText.text = "No output currently available...";
        taskNumberText.text = "";
        taskShapeList.GetChild(0).gameObject.SetActive(false);
        taskShapeList.GetChild(1).gameObject.SetActive(false);
        taskShapeList.GetChild(2).gameObject.SetActive(false);
        taskShapeList.GetChild(3).gameObject.SetActive(false);
        taskColorImage.color = new Color(0f, 0f, 0f, 0f);

        currentShapeSelection = cube;
        //ResetCubeGrabbable(); //only useful for user resetting level with A button
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

        stationText.text = "Output generated\nPlease take to task station";
        cubeGrabbable.GetComponent<Collider>().enabled = true;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = true;
        Debug.Log(number.ToString() + currentColor.ToString() + shape.ToString());

        if (shape == "Cube") currentShapeSelection = cube;
        if (shape == "Sphere") currentShapeSelection = sphere;
        if (shape == "Cone") currentShapeSelection = cone;
        if (shape == "Ring") currentShapeSelection = ring;

        /* May want floating objects in the top container for future versionx
        //destroy objects floating in output station
        foreach (Transform child in spawnLocation)
        {
            Destroy(child.gameObject);
        }
        */

        //generate objects in carrying container
        for (int i = 0; i < number; i++)
        {
            objectSpawn = Instantiate(currentShapeSelection);
            objectPosition = new Vector3(cubeGrabbable.position.x + Random.Range(-0.05f, 0.05f), 
                cubeGrabbable.position.y + Random.Range(0.3f, 0.6f), cubeGrabbable.position.z + Random.Range(-0.05f, 0.05f));
            objectSpawn.transform.position = objectPosition;
            objectSpawn.transform.localScale *= Random.Range(1.5f, 3f);
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
    }

    //was used when I spawned 3d objects to show the current output state from across the room
    private void ObjectSpawner(float xOffset, float yOffset, float zOffset)
    {
        objectSpawn = Instantiate(currentShapeSelection);
        objectSpawn.transform.parent = spawnLocation;
        objectSpawn.transform.position = new Vector3(spawnLocation.position.x + xOffset, spawnLocation.position.y + yOffset, spawnLocation.position.z + zOffset);
        objectSpawn.GetComponent<RandomMovement>().enabled = true;
        objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
        objectSpawn.transform.localScale *= 5f;
    }

    public void UpdateNumText(int number, int currentTask)
    {
        if (number > 0)
        {
            stationText.text = "Pull lever to generate output:";
            taskNumberText.text = LevelManager.instance.GetNumber().ToString();
            UpdateColorText(LevelManager.instance.GetColor(), LevelManager.instance.GetColorText());
            UpdateShapeText(LevelManager.instance.GetShape());

            //for tutorial task, wait to turn on leverRunOutput
            if(currentTask > 1) leverRunOutput.Activate();

            if (LevelManager.instance.GetShape() == "Cube") currentShapeSelection = cube;
            if (LevelManager.instance.GetShape() == "Sphere") currentShapeSelection = sphere;
            if (LevelManager.instance.GetShape() == "Cone") currentShapeSelection = cone;
            if (LevelManager.instance.GetShape() == "Ring") currentShapeSelection = ring;

            //was used when I spawned 3d objects to show the current output state from across the room
            /*
            //destroy current objects
            foreach (Transform child in spawnLocation)
            {
                Destroy(child.gameObject);
            }
            //build new objects
            switch (number)
            {
                case 1:
                    ObjectSpawner(0f, 0f, 0f);
                    break;

                case 2:
                    ObjectSpawner(0f, 0f, -0.1f);
                    ObjectSpawner(0f, 0f, 0.1f);
                    break;

                case 3:
                    ObjectSpawner(0f, 0f, -0.2f);
                    ObjectSpawner(0f, 0f, 0.2f);
                    ObjectSpawner(0f, 0f, 0f);
                    break;

                case 4:
                    ObjectSpawner(0f, 0f, -0.1f);
                    ObjectSpawner(0f, 0f, 0.1f);
                    ObjectSpawner(0f, 0f, -0.3f);
                    ObjectSpawner(0f, 0f, 0.3f);
                    break;

                case 5:
                    ObjectSpawner(0f, 0f, -0.1f);
                    ObjectSpawner(0f, 0f, 0.1f);
                    ObjectSpawner(0f, -0.15f, -0.2f);
                    ObjectSpawner(0f, -0.15f, 0.2f);
                    ObjectSpawner(0f, -0.15f, 0f);
                    break;

                case 6:
                    ObjectSpawner(0f, 0f, 0f);
                    ObjectSpawner(0f, 0f, -0.2f);
                    ObjectSpawner(0f, 0f, 0.2f);
                    ObjectSpawner(0f, -0.15f, -0.2f);
                    ObjectSpawner(0f, -0.15f, 0.2f);
                    ObjectSpawner(0f, -0.15f, 0f);
                    break;

                case 7:
                    ObjectSpawner(0f, 0f, 0f);
                    ObjectSpawner(0f, 0f, -0.2f);
                    ObjectSpawner(0f, 0f, 0.2f);
                    ObjectSpawner(0f, -0.15f, -0.1f);
                    ObjectSpawner(0f, -0.15f, 0.1f);
                    ObjectSpawner(0f, -0.15f, -0.3f);
                    ObjectSpawner(0f, -0.15f, 0.3f);
                    break;

                default:
                    break;
            }
            */
        }
        else
        {
            stationText.text = "No output currently available...";
            taskNumberText.text = "";
            taskShapeList.GetChild(0).gameObject.SetActive(false);
            taskShapeList.GetChild(1).gameObject.SetActive(false);
            taskShapeList.GetChild(2).gameObject.SetActive(false);
            taskShapeList.GetChild(3).gameObject.SetActive(false);
            taskColorImage.color = new Color(0f, 0f, 0f, 0f);
            leverRunOutput.SetTask();

            //was used when I spawned 3d objects to show the current output state from across the room
            /*
            //destroy current objects
            foreach (Transform child in spawnLocation)
            {
                Destroy(child.gameObject);
            }
            */
        }
    }

    public void UpdateShapeText(string shape)
    {
        if (shape == "Cube")
        {
            taskShapeList.GetChild(0).gameObject.SetActive(true);
            taskShapeList.GetChild(1).gameObject.SetActive(false);
            taskShapeList.GetChild(2).gameObject.SetActive(false);
            taskShapeList.GetChild(3).gameObject.SetActive(false);
        }
        if (shape == "Sphere")
        {
            taskShapeList.GetChild(0).gameObject.SetActive(false);
            taskShapeList.GetChild(1).gameObject.SetActive(true);
            taskShapeList.GetChild(2).gameObject.SetActive(false);
            taskShapeList.GetChild(3).gameObject.SetActive(false);
        }
        if (shape == "Cone")
        {
            taskShapeList.GetChild(0).gameObject.SetActive(false);
            taskShapeList.GetChild(1).gameObject.SetActive(false);
            taskShapeList.GetChild(2).gameObject.SetActive(true);
            taskShapeList.GetChild(3).gameObject.SetActive(false);
        }
        if (shape == "Ring")
        {
            taskShapeList.GetChild(0).gameObject.SetActive(false);
            taskShapeList.GetChild(1).gameObject.SetActive(false);
            taskShapeList.GetChild(2).gameObject.SetActive(false);
            taskShapeList.GetChild(3).gameObject.SetActive(true);
        }

        //was used when I spawned 3d objects to show the current output state from across the room
        /*
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
                Debug.Log("Went through spawnLocation "+ i.ToString());
                objectSpawn = Instantiate(currentShapeSelection);
                objectSpawn.transform.position = spawnLocation.GetChild(i).position;
                objectSpawn.transform.localScale = spawnLocation.GetChild(i).localScale;
                objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = LevelManager.instance.GetColor();
                objectSpawn.transform.parent = tempSpawnLocation;
                objectSpawn.GetComponent<RandomMovement>().enabled = true;
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
        */
    }

    public void UpdateColorText(Color currentColor, string currentColorText)
    {
        //colorItem.text = currentColorText;
        taskColorImage.color = currentColor;

        //was used when I spawned 3d objects to show the current output state from across the room
        /*
        //change color for all spawned 3D objects
        foreach (Transform child in spawnLocation)
        {
            child.GetChild(0).GetComponent<Renderer>().material.color = currentColor;
        }
        */
    }

    private void ResetCubeGrabbable() //Place carrying container back to original location 
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
    }
}
