using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputStation : MonoBehaviour
{
    public Text stationText;
    public Transform cubeGrabbable, cubeGrabbleStartPosition, spawnLocation, tempSpawnLocation, farLocation, nearLocation;
    public LeverPulled leverRunOutput;
    public GameObject cube, sphere, cone, ring, currentShapeSelection;
    public GameObject[] objectIconsLarge;
    public Text taskNumberText;
    public Transform taskShapeList;
    public Image taskColorImage;

    private GameObject objectSpawn;
    private Vector3 objectPosition;
    private int spawnedObjects;

    private void Start()
    {
        //if (Application.isEditor) transform.position = nearLocation.position;
    }

    //Currently not using, meant for Rift testing, but now use quick move feature
    //Sets distance of tutorial walls based on 20'x20' or 12'x12' space
    public void SetLevelDistance(bool isNear)
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
    }

    public void GetTaskLeverPulled(int currentTask)
    {
        ResetCarryingContainer();
    }

    public void RunOutputLeverPulled(int number, Color currentColor, string shape)
    {
        //to reset the sendOutputLever
        LevelManager.instance.RunOutput(); 

        stationText.text = "Output generated\nPlease take to task station";
        cubeGrabbable.GetComponent<Collider>().enabled = true;
        cubeGrabbable.GetComponent<OVRGrabbable>().enabled = true;
        Debug.Log(number.ToString() + currentColor.ToString() + shape.ToString());

        if (shape == "Cube") currentShapeSelection = cube;
        if (shape == "Sphere") currentShapeSelection = sphere;
        if (shape == "Cone") currentShapeSelection = cone;
        if (shape == "Ring") currentShapeSelection = ring;

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
    }

    public void UpdateColorText(Color currentColor, string currentColorText)
    {
        taskColorImage.color = currentColor;
    }

    private void ResetCarryingContainer() 
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
